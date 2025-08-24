using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Refund
    {
        [Key]
        public Guid RefundId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public Guid PaymentId { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal RefundAmount { get; set; }

        [Required]
        public string RefundReason { get; set; }

        public int RefundStatus { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }

        [StringLength(100)]
        public string ProcessedBy { get; set; }

        // Navigation Properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}
