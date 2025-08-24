using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class VehicleRentalBookingRepository : Repository<VehicleRentalBooking>, IVehicleRentalBookingRepository
    {
        public VehicleRentalBookingRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for VehicleRentalBooking if needed
    }
} 