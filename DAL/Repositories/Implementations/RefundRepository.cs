using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class RefundRepository : Repository<Refund>, IRefundRepository
    {
        public RefundRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for Refund if needed
    }
} 