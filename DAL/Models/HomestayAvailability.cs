using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class HomestayAvailability
    {
        [Key]
        public Guid AvailabilityId { get; set; }

        [Required]
        public Guid HomestayId { get; set; }

        public Guid? RoomId { get; set; } // Null if applies to entire homestay

        [Required]
        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int MinNights { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("HomestayId")]
        public virtual HomestayService HomestayService { get; set; }

        [ForeignKey("RoomId")]
        public virtual HomestayRoom HomestayRoom { get; set; }
    }
}
