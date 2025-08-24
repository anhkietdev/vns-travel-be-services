using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IVehicleRentalService
    {
        Task<ICollection<VehicleRentalService>> GetAllAsync();
        Task<VehicleRentalService?> GetByIdAsync(Guid id);
        Task AddAsync(VehicleRentalService entity);
        Task UpdateAsync(VehicleRentalService entity);
        Task DeleteAsync(Guid id);
    }
} 