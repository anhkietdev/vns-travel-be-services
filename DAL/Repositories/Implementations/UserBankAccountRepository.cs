using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class UserBankAccountRepository : Repository<UserBankAccount>, IUserBankAccountRepository
    {
        public UserBankAccountRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for UserBankAccount if needed
    }
} 