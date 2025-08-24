using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TourItinerary
    {
        [Key]
        public Guid ItineraryId { get; set; }

        [Required]
        public Guid TourId { get; set; }

        public int StepOrder { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        [StringLength(255)]
        public string Activity { get; set; }

        public int DurationMinutes { get; set; }

        public string Description { get; set; }

        // Navigation Properties
        [ForeignKey("TourId")]
        public virtual TourService TourService { get; set; }
    }
}
