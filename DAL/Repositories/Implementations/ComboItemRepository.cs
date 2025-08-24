using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class ComboItemRepository : Repository<ComboItem>, IComboItemRepository
    {
        public ComboItemRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for ComboItem if needed
    }
} 