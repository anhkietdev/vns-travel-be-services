using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Commons;
using DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BLL.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Service>> GetServices(ServiceFilterDto filterDto)
        {            
            Expression<Func<Service, bool>>? filter = null;

            if (filterDto != null)
            {
                filter = s =>
                    (!filterDto.ServiceId.HasValue || s.ServiceId == filterDto.ServiceId) &&
                    (!filterDto.LocationId.HasValue || s.LocationId == filterDto.LocationId) &&
                    (!filterDto.PartnerId.HasValue || s.PartnerId == filterDto.PartnerId) &&
                    (string.IsNullOrEmpty(filterDto.Title) || s.Title.Contains(filterDto.Title));
            }

            Expression<Func<Service, object>> orderBy = s => s.Title;

            var services = await _unitOfWork.Service.GetAllAsync(
                filter: filter,
                orderBy: orderBy,
                sortDirection: SortDirection.Ascending,
                tracked: false
            );

            return services;
        }



        public async Task CreateService(ServiceDto serviceDto)
        {
            Service service = _mapper.Map<Service>(serviceDto);
            await _unitOfWork.Service.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateService(ServiceDto serviceDto)
        {
            var service = await _unitOfWork.Service.GetAsync(s => s.ServiceId == serviceDto.ServiceId);
            if (service == null)
            {
                throw new KeyNotFoundException("Service not found");
            }
            _mapper.Map(serviceDto, service);
            await _unitOfWork.Service.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteService(Guid serviceId)
        {
            var service = await _unitOfWork.Service.GetAsync(s => s.ServiceId == serviceId);
            if (service == null)
            {
                throw new KeyNotFoundException("Service not found");
            }
            await _unitOfWork.Service.RemoveAsync(service);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Service> GetServiceById(Guid serviceId)
        {
            var service = await _unitOfWork.Service.GetAsync(s => s.ServiceId == serviceId);
            if (service == null)
            {
                throw new KeyNotFoundException("Service not found");
            }
            return service;
        }
    }
}
