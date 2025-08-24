using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class FinancialReport
    {
        [Key]
        public Guid FinancialReportId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        public DateTime ReportDate { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal TotalRevenue { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal TotalRefund { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal PlatformFees { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal NetRevenue { get; set; }

        // Navigation Properties
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }
    }
}
