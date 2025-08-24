using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class PartnerDocumentRepository : Repository<PartnerDocument>, IPartnerDocumentRepository
    {
        public PartnerDocumentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
