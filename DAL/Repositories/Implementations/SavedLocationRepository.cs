using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class SavedLocationRepository : Repository<SavedLocation>, ISavedLocationRepository
    {
        public SavedLocationRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for SavedLocation if needed
    }
} 