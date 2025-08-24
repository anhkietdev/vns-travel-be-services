using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string District { get; set; }

        [StringLength(100)]
        public string Ward { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [Column(TypeName = "decimal(10,8)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(11,8)")]
        public decimal? Longitude { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public string OpeningHours { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<PartnerLocation> PartnerLocations { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<SavedLocation> SavedLocations { get; set; }
        public virtual ICollection<ServiceRating> ServiceRatings { get; set; }
    }
}
