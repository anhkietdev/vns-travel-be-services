using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class FinancialReportRepository : Repository<FinancialReport>, IFinancialReportRepository
    {
        public FinancialReportRepository(AppDbContext context) : base(context)
        {
        }
    }
}
