using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class TourItineraryRepository : Repository<TourItinerary>, ITourItineraryRepository
    {
        public TourItineraryRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for TourItinerary if needed
    }
} 