namespace BLL.DTOs.Tour
{
    public class CreateTourItineraryDto
    {
        public Guid TourId { get; set; }
        public int StepOrder { get; set; }
        public string Location { get; set; }
        public string Activity { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
    }
}
