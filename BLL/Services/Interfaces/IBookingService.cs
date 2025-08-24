using BLL.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync(Guid userId);
        Task<BookingDto> GetBookingAsync(Guid bookingId);
        Task CreateBookingAsync(BookingDto bookingDto);
        Task UpdateBookingAsync(Guid bookingId);
        Task<bool> DeleteBookingAsync(Guid bookingId);
    }
}
