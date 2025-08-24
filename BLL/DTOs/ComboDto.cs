namespace BLL.DTOs
{
    public class ComboDto
    {
        public Guid ComboId { get; set; }
        public Guid PartnerId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal DiscountedPrice { get; set; }
        public int? Availability { get; set; }
        public string Location { get; set; } = default!;
        public List<ComboServiceDto> ComboServices { get; set; } = new();
    }
}
