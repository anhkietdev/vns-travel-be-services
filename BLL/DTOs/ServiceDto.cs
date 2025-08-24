using DAL.Models.Enum;

namespace BLL.DTOs
{
    public class ServiceDto
    {
        public Guid ServiceId { get; set; }
        public Guid PartnerId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int? Availability { get; set; }
        public ServiceType ServiceType { get; set; }
        public required string Location { get; set; }
    }
}
