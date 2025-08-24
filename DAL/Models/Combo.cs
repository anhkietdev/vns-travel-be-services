using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Combo
    {
        [Key]
        public Guid ComboId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal OriginalPrice { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal DiscountedPrice { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public int MaxBookings { get; set; }
        public int CurrentBookings { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        public virtual ICollection<ComboItem> ComboItems { get; set; }
    }
}
