using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        public int ServiceType { get; set; } // 1: homestay, 2: vehicle_rental, 3: tour

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual HomestayService HomestayService { get; set; }
        public virtual VehicleRentalService VehicleRentalService { get; set; }
        public virtual TourService TourService { get; set; }

        public virtual ICollection<ComboItem> ComboItems { get; set; }
        public virtual ICollection<ServicePromotion> ServicePromotions { get; set; }
        public virtual ICollection<ServiceFeedback> ServiceFeedbacks { get; set; }
        public virtual ICollection<ServiceRating> ServiceRatings { get; set; }
    }
}
