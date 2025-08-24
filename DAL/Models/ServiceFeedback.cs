using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ServiceFeedback
    {
        [Key]
        public Guid FeedbackId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        public int ServiceType { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Range(1, 5)]
        public int OverallRating { get; set; }

        [Range(1, 5)]
        public int? CleanlinessRating { get; set; } // For accommodation

        [Range(1, 5)]
        public int? ServiceRating { get; set; }

        [Range(1, 5)]
        public int? ValueRating { get; set; }

        [Range(1, 5)]
        public int? LocationRating { get; set; }

        public string Comment { get; set; }
        public string Images { get; set; } // JSON array

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }
    }
}
