using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class HomestayAvailabilityRepository : Repository<HomestayAvailability>, IHomestayAvailabilityRepository
    {
        public HomestayAvailabilityRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for HomestayAvailability if needed
    }
} 