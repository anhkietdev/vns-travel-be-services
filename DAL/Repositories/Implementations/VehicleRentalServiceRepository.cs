using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class VehicleRentalServiceRepository : Repository<VehicleRentalService>, IVehicleRentalServiceRepository
    {
        public VehicleRentalServiceRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for VehicleRentalService if needed
    }
} 