using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ServicePromotion
    {
        [Key]
        public Guid PromotionId { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
        public int ServiceType { get; set; }
        public int PromotionType { get; set; } // 1: percentage, 2: fixed_amount, 3: buy_x_get_y
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxUsage { get; set; }
        public int CurrentUsage { get; set; }
        public string Conditions { get; set; } // JSON
        // Navigation Properties
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}
