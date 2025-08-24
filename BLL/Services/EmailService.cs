using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public interface IEmailService
    {
        Task SendOTPEmailAsync(string to, string otp);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpServer = _configuration["Email:SmtpServer"]!;
            _smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
            _smtpUsername = _configuration["Email:SmtpUsername"]!;
            _smtpPassword = _configuration["Email:SmtpPassword"]!;
            _fromEmail = _configuration["Email:FromEmail"]!;
            _fromName = _configuration["Email:FromName"]!;
        }

        public async Task SendOTPEmailAsync(string to, string otp)
        {
            using var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword)
            };

            var message = new MailMessage
            {
                From = new MailAddress(_fromEmail, _fromName),
                Subject = "Password Reset OTP",
                Body = $@"
                    <h2>Password Reset Request</h2>
                    <p>You have requested to reset your password. Please use the following OTP to proceed:</p>
                    <h1 style='color: #4CAF50; font-size: 32px; letter-spacing: 5px;'>{otp}</h1>
                    <p>This OTP will expire in 5 minutes.</p>
                    <p>If you did not request this password reset, please ignore this email.</p>",
                IsBodyHtml = true
            };

            message.To.Add(to);

            await client.SendMailAsync(message);
        }
    }
} 