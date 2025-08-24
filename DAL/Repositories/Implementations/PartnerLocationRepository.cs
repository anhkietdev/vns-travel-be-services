using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class PartnerLocationRepository : Repository<PartnerLocation>, IPartnerLocationRepository
    {
        public PartnerLocationRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for PartnerLocation if needed
    }
} 