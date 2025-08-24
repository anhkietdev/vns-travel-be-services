using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                throw new ArgumentNullException(nameof(bookingDto), "Booking data cannot be null");
            }
            var booking = _mapper.Map<Booking>(bookingDto);
            await _unitOfWork.Booking.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteBookingAsync(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
            {
                throw new ArgumentException("Booking ID cannot be empty", nameof(bookingId));
            }
            var booking = await _unitOfWork.Booking.GetAsync(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }
            await _unitOfWork.Booking.RemoveAsync(booking);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty", nameof(userId));
            }
            var bookings = await _unitOfWork.Booking.GetAllAsync(b => b.UserId == userId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingAsync(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
            {
                throw new ArgumentException("Booking ID cannot be empty", nameof(bookingId));
            }
            var booking = await _unitOfWork.Booking.GetAsync(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task UpdateBookingAsync(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
            {
                throw new ArgumentException("Booking ID cannot be empty", nameof(bookingId));
            }
            var booking = await _unitOfWork.Booking.GetAsync(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }

            await _unitOfWork.Booking.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
