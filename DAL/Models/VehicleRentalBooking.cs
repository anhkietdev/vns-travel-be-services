using DAL.Models.Enum;

namespace DAL.Models
{
    public class VehicleRentalBooking
    {
        public Guid VehicleRentalBookingId { get; set; }
        public Guid BookingId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime RentalStartTime { get; set; }
        public DateTime RentalEndTime { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string ReturnLocation { get; set; } = string.Empty;
        public bool DriverRequired { get; set; } = false;
        public required string DriverIdentification { get; set; }
        public decimal DepositPaid { get; set; } 
        public decimal TotalPrice { get; set; }
        public int RentalHours { get; set; } 
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
    }
}
