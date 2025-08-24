using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class VehicleCategoryRepository : Repository<VehicleCategory>, IVehicleCategoryRepository
    {
        public VehicleCategoryRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for VehicleCategory if needed
    }
} 