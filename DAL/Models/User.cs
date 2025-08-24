using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        public string? AvatarUrl { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<UserBankAccount> BankAccounts { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<SavedLocation> SavedLocations { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<ServiceFeedback> ServiceFeedbacks { get; set; }
    }
}
