using DAL.Models.Enum;

namespace BLL.DTOs
{
    public class BookingDto
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ComboId { get; set; }
        public BookingType BookingType { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
