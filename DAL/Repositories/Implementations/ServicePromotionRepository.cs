using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class ServicePromotionRepository : Repository<ServicePromotion>, IServicePromotionRepository
    {
        public ServicePromotionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
