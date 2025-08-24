using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class HomestayBookingRepository : Repository<HomestayBooking>, IHomestayBookingRepository
    {
        public HomestayBookingRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for HomestayBooking if needed
    }
} 