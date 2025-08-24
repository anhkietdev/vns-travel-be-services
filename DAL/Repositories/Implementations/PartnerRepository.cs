using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
