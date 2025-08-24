using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class ComboRepository : Repository<Combo>, IComboRepository
    {
        public ComboRepository(AppDbContext context) : base(context)
        {
        }
    }
}
