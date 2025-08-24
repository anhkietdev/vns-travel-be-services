using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Booking data is null");
            }
            try
            {
                await _bookingService.CreateBookingAsync(bookingDto);
                return Ok("Booking created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBooking(Guid bookingId)
        {
            try
            {
                var booking = await _bookingService.GetBookingAsync(bookingId);
                if (booking == null)
                {
                    return NotFound("Booking not found");
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings(Guid userId)
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync(userId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(Guid bookingId)
        {
            try
            {
                await _bookingService.UpdateBookingAsync(bookingId);
                return Ok("Booking updated successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(Guid bookingId)
        {
            try
            {
                var result = await _bookingService.DeleteBookingAsync(bookingId);
                if (!result)
                {
                    return NotFound("Booking not found");
                }
                return Ok("Booking deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
