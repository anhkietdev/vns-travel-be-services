using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class TourScheduleRepository : Repository<TourSchedule>, ITourScheduleRepository
    {
        public TourScheduleRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for TourSchedule if needed
    }
} 