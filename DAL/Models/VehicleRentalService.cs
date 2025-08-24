using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class VehicleRentalService
    {
        [Key]
        public Guid RentalId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        public int RentalType { get; set; } // 1: self_drive, 2: with_driver, 3: both

        [StringLength(255)]
        public string BusinessLicense { get; set; }

        public string InsurancePolicy { get; set; }
        public string OperatingHours { get; set; } // JSON
        public string PickupLocations { get; set; } // JSON

        public bool DeliveryAvailable { get; set; }

        public int MinRentalHours { get; set; }
        public int MaxRentalDays { get; set; }

        // Fuel policy
        [StringLength(500)]
        public string FuelPolicy { get; set; } = "Full tank handover - customer responsible for fuel during rental";

        [Column(TypeName = "decimal(5,2)")]
        public decimal FuelTankCapacity { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal EstimatedFuelConsumption { get; set; }

        // Additional fees
        [Column(TypeName = "decimal(10,2)")]
        public decimal LateReturnFeePerHour { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CleaningFee { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SmokingPenalty { get; set; }

        // Navigation Properties
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        public virtual ICollection<VehicleCategory> VehicleCategories { get; set; }
    }
}
