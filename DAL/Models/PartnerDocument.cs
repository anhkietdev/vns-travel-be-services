using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PartnerDocument
    {
        [Key]
        public Guid PartnerDocumentId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required]
        public string DocumentUrl { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }

        [StringLength(255)]
        public string VerifiedBy { get; set; }

        // Navigation Properties
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }
    }
}
