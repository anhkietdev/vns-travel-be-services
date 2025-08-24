using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class RevenueRepository : Repository<Revenue>, IRevenueRepository
    {
        public RevenueRepository(AppDbContext context) : base(context)
        {
        }
    }
}
