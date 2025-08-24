using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class VehicleCategory
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid RentalId { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        // Navigation Properties
        [ForeignKey("RentalId")]
        public virtual VehicleRentalService VehicleRentalService { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
