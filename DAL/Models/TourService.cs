using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TourService
    {
        [Key]
        public Guid TourId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        public int TourType { get; set; } // 1: city_tour, 2: nature, 3: cultural, 4: adventure

        public int DurationHours { get; set; }
        public int DifficultyLevel { get; set; } // 1: easy, 2: moderate, 3: hard

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }

        public string Includes { get; set; } // JSON
        public string Excludes { get; set; } // JSON
        public string WhatToBring { get; set; }
        public string CancellationPolicy { get; set; }

        [StringLength(255)]
        public string AgeRestrictions { get; set; }

        public string FitnessRequirements { get; set; }

        // Navigation Properties
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        public virtual ICollection<TourSchedule> TourSchedules { get; set; }
        public virtual ICollection<TourItinerary> TourItineraries { get; set; }

    }
}
