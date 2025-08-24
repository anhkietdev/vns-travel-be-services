using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
