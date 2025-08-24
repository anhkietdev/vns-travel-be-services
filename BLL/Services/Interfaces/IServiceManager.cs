using BLL.DTOs;
using DAL.Models;

namespace BLL.Services.Interfaces
{
    public interface IServiceManager
    {
        Task<IEnumerable<Service>> GetServices(ServiceFilterDto filterDto);
        Task<Service> GetServiceById(Guid serviceId);
        Task CreateService(ServiceDto serviceDto);
        Task UpdateService(ServiceDto serviceDto);
        Task<bool> DeleteService(Guid serviceId);
    }
}
