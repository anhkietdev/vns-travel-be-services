namespace DAL.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public string TransmissionType { get; set; }
        public int Seats { get; set; }
        public string FuelType { get; set; }
        public int LittersPer100Km { get; set; }
        public List<string> Features { get; set; }
        public List<string> Images { get; set; }
        public string Status { get; set; }
    }
}
