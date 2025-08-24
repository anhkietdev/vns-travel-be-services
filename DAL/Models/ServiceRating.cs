using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ServiceRating
    {
        [Key]
        public Guid RatingId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        public int ServiceType { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; }

        public int TotalReviews { get; set; }

        public string RatingBreakdown { get; set; } // JSON

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}
