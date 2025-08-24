using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class HomestayServiceRepository : Repository<HomestayService>, IHomestayServiceRepository
    {
        public HomestayServiceRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for HomestayService if needed
    }
} 