using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class TourBookingRepository : Repository<TourBooking>, ITourBookingRepository
    {
        public TourBookingRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for TourBooking if needed
    }
} 