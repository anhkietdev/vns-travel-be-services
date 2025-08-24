using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class HomestayBooking
    {
        [Key]
        public Guid HomestayBookingId { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public Guid HomestayId { get; set; }

        public Guid? RoomId { get; set; } // Null if entire house

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public int Nights { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }

        public int BookingType { get; set; } // 1: entire_house, 2: private_room, 3: shared_room

        [Column(TypeName = "decimal(10,2)")]
        public decimal RoomRate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CleaningFee { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ServiceFee { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal TotalAccommodationCost { get; set; }

        public bool HostApprovalRequired { get; set; }
        public DateTime? HostApprovedAt { get; set; }

        // Navigation Properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        [ForeignKey("HomestayId")]
        public virtual HomestayService HomestayService { get; set; }

        [ForeignKey("RoomId")]
        public virtual HomestayRoom HomestayRoom { get; set; }
    }
}
