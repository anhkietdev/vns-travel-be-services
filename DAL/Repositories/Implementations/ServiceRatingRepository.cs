using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class ServiceRatingRepository : Repository<ServiceRating>, IServiceRatingRepository
    {
        public ServiceRatingRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for ServiceRating if needed
    }
} 