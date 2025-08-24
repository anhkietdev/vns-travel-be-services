using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Voucher
    {
        [Key]
        public Guid VoucherId { get; set; }

        public Guid? UserId { get; set; } // Null if public voucher

        [Required]
        [StringLength(50)]
        public string VoucherCode { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? DiscountPercentage { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? DiscountAmount { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public string ServiceTypes { get; set; } // JSON array

        public bool IsPublic { get; set; }

        public int MaxUsage { get; set; }
        public int CurrentUsage { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
