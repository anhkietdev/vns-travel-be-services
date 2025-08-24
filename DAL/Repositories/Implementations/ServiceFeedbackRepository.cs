using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class ServiceFeedbackRepository : Repository<ServiceFeedback>, IServiceFeedbackRepository
    {
        public ServiceFeedbackRepository(AppDbContext context) : base(context)
        {
        }
        // Add custom methods for ServiceFeedback if needed
    }
} 