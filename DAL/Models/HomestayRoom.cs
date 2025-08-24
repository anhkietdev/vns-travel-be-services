using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class HomestayRoom
    {
        [Key]
        public Guid RoomId { get; set; }

        [Required]
        public Guid HomestayId { get; set; }

        [Required]
        [StringLength(255)]
        public string RoomName { get; set; }

        public string RoomDescription { get; set; }

        public int MaxOccupancy { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? RoomSizeSqm { get; set; }

        [StringLength(100)]
        public string BedType { get; set; }

        public int BedCount { get; set; }

        public bool PrivateBathroom { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal WeekendPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal HolidayPrice { get; set; }

        public string RoomAmenities { get; set; } // JSON
        public string Images { get; set; } // JSON

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("HomestayId")]
        public virtual HomestayService HomestayService { get; set; }

        public virtual ICollection<HomestayAvailability> HomestayAvailabilities { get; set; }
        public virtual ICollection<HomestayBooking> HomestayBookings { get; set; }
    }
}
