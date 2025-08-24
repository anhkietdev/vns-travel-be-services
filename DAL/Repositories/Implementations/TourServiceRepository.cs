using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class TourServiceRepository : Repository<TourService>, ITourServiceRepository
    {
        public TourServiceRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for TourService if needed
    }
} 