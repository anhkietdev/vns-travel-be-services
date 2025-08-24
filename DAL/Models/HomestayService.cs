using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class HomestayService
    {
        [Key]
        public Guid HomestayId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        public int HomestayType { get; set; } // 1: entire_house, 2: private_room, 3: shared_room
        public int PropertyType { get; set; } // 1: house, 2: apartment, 3: villa, 4: bungalow

        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }

        public string CancellationPolicy { get; set; }
        public string HouseRules { get; set; } // JSON
        public string Amenities { get; set; } // JSON
        public string HostInfo { get; set; } // JSON

        // Navigation Properties
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        public virtual ICollection<HomestayRoom> HomestayRooms { get; set; }
        public virtual ICollection<HomestayAvailability> HomestayAvailabilities { get; set; }
        public virtual ICollection<HomestayBooking> HomestayBookings { get; set; }

    }
}
