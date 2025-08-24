using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class HomestayRoomRepository : Repository<HomestayRoom>, IHomestayRoomRepository
    {
        public HomestayRoomRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for HomestayRoom if needed
    }
} 