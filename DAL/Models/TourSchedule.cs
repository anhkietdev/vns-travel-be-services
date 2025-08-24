using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TourSchedule
    {
        [Key]
        public Guid ScheduleId { get; set; }

        [Required]
        public Guid TourId { get; set; }

        [Required]
        public DateTime TourDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int AvailableSlots { get; set; }
        public int BookedSlots { get; set; }

        [StringLength(100)]
        public string GuideId { get; set; }

        [StringLength(255)]
        public string MeetingPoint { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        [ForeignKey("TourId")]
        public virtual TourService TourService { get; set; }

        public virtual ICollection<TourBooking> TourBookings { get; set; }
    }
}
