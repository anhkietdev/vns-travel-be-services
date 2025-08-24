using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class VehicleRentalServiceService : IVehicleRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleRentalServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<VehicleRentalService>> GetAllAsync() => await _unitOfWork.VehicleRentalService.GetAllAsync();
        public async Task<VehicleRentalService?> GetByIdAsync(Guid id) => await _unitOfWork.VehicleRentalService.GetAsync(x => x.RentalId == id);
        public async Task AddAsync(VehicleRentalService entity) { await _unitOfWork.VehicleRentalService.AddAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task UpdateAsync(VehicleRentalService entity) { await _unitOfWork.VehicleRentalService.UpdateAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var entity = await _unitOfWork.VehicleRentalService.GetAsync(x => x.RentalId == id); if (entity != null) { await _unitOfWork.VehicleRentalService.RemoveAsync(entity); await _unitOfWork.SaveChangesAsync(); } }
    }
} 