using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Revenue
    {
        [Key]
        public Guid RevenueId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        public int ServiceType { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal TotalEarnings { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal PlatformFee { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal NetEarnings { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        // Navigation Properties
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }
    }
}
