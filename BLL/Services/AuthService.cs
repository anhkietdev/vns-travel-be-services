using BLL.DTOs;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BLL.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO model);
        Task<AuthResponseDTO> LoginAsync(LoginDTO model);
        Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken);
        Task SendForgotPasswordOTPAsync(ForgotPasswordDTO model);
        Task<bool> VerifyOTPAsync(VerifyOTPDTO model);
        Task ResetPasswordAsync(ResetPasswordDTO model);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;
        private static readonly Dictionary<string, (string OTP, DateTime Expiry)> _otpStore = new();

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IPasswordHasher<User> passwordHasher,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new Exception("Password is required.");
            }
            var existingUser = await _unitOfWork.User.GetAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                FullName = $"{model.FullName}",
                PhoneNumber = model.PhoneNumber,
                Role = (int)Role.Customer, // Default role
                Password = _passwordHasher.HashPassword(null!, model.Password)
            };

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return new AuthResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.Now.AddHours(24), // Match with JWT expiration
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _unitOfWork.User.GetAsync(u => u.Email == model.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid email or password");
            }

            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return new AuthResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.Now.AddHours(24),
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                throw new Exception("Invalid refresh token");
            }

            var userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var user = await _unitOfWork.User.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var newToken = _jwtService.GenerateJwtToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            return new AuthResponseDTO
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.Now.AddHours(24),
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task SendForgotPasswordOTPAsync(ForgotPasswordDTO model)
        {
            var user = await _unitOfWork.User.GetAsync(u => u.Email == model.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Generate 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(5);

            // Store OTP with expiry
            _otpStore[model.Email] = (otp, expiry);

            // Send OTP via email
            await _emailService.SendOTPEmailAsync(model.Email, otp);
        }

        public async Task<bool> VerifyOTPAsync(VerifyOTPDTO model)
        {
            if (!_otpStore.TryGetValue(model.Email, out var otpData))
            {
                throw new Exception("OTP not found or expired");
            }

            if (DateTime.UtcNow > otpData.Expiry)
            {
                _otpStore.Remove(model.Email);
                throw new Exception("OTP has expired");
            }

            if (otpData.OTP != model.OTP)
            {
                throw new Exception("Invalid OTP");
            }

            return true;
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO model)
        {
            // Verify OTP first
            await VerifyOTPAsync(new VerifyOTPDTO { Email = model.Email, OTP = model.OTP });

            var user = await _unitOfWork.User.GetAsync(u => u.Email == model.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update password
            user.Password = _passwordHasher.HashPassword(user, model.NewPassword);
            await _unitOfWork.User.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Remove used OTP
            _otpStore.Remove(model.Email);
        }
    }
} 