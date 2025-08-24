namespace BLL.DTOs
{
    public class ServiceFilterDto
    {
        public Guid? ServiceId { get; set; }
        public Guid? PartnerId { get; set; }
        public string? Title { get; set; }
        public Guid? LocationId { get; set; }
    }
}
