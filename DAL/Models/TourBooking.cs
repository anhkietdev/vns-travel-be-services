using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TourBooking
    {
        [Key]
        public Guid TourBookingId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public Guid ScheduleId { get; set; }

        public int Participants { get; set; }
        public string ParticipantDetails { get; set; } // JSON

        [StringLength(255)]
        public string PickupLocation { get; set; }

        public TimeSpan? PickupTime { get; set; }

        // Navigation Properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual TourSchedule TourSchedule { get; set; }
    }
}
