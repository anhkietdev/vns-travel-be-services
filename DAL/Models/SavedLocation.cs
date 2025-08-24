using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class SavedLocation
    {
        [Key]
        public Guid SavedLocationId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [StringLength(100)]
        public string AliasName { get; set; } // "Home", "Work", etc.

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}
