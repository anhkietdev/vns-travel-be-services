using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal Amount { get; set; }

        public int PaymentType { get; set; } // 1: deposit, 2: full_payment, 3: remaining

        public DateTime PaymentTime { get; set; }

        [Required]
        [StringLength(255)]
        public string TransactionId { get; set; }

        public int PaymentMethod { get; set; }
        public int PaymentStatus { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal RefundAmount { get; set; }

        public string RefundReason { get; set; }
        public DateTime? RefundedAt { get; set; }

        // Navigation Properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
