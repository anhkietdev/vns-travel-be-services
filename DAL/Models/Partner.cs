using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Partner
    {
        [Key]
        public Guid PartnerId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string BusinessName { get; set; }

        [StringLength(100)]
        public string BusinessCategory { get; set; }

        public string ContactInfo { get; set; }
        public string Description { get; set; }

        public bool IsVerified { get; set; }
        public int VerificationStatus { get; set; }

        [StringLength(50)]
        public string TaxCode { get; set; }

        [StringLength(100)]
        public string BusinessLicenseNumber { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal PlatformFeePercentage { get; set; }

        public int PlatformFeeType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<PartnerDocument> Documents { get; set; }
        public virtual ICollection<PartnerLocation> PartnerLocations { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Combo> Combos { get; set; }
        public virtual ICollection<Revenue> Revenues { get; set; }
        public virtual ICollection<FinancialReport> FinancialReports { get; set; }
    }
}
