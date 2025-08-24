using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ComboItem
    {
        [Key]
        public Guid ComboItemId { get; set; }
        [Required]
        public Guid ComboId { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
        public int ServiceType { get; set; }
        public int Quantity { get; set; }
        public string IncludedFeatures { get; set; } // JSON
        public int SequenceOrder { get; set; }
        // Navigation Properties
        [ForeignKey("ComboId")]
        public virtual Combo Combo { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}
