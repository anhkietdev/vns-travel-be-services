using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implementations
{
    public class HomestayServiceService : IHomestayService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomestayServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<HomestayService>> GetAllAsync() => await _unitOfWork.HomestayService.GetAllAsync();
        public async Task<HomestayService?> GetByIdAsync(Guid id) => await _unitOfWork.HomestayService.GetAsync(x => x.HomestayId == id);
        public async Task AddAsync(HomestayService entity) { await _unitOfWork.HomestayService.AddAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task UpdateAsync(HomestayService entity) { await _unitOfWork.HomestayService.UpdateAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var entity = await _unitOfWork.HomestayService.GetAsync(x => x.HomestayId == id); if (entity != null) { await _unitOfWork.HomestayService.RemoveAsync(entity); await _unitOfWork.SaveChangesAsync(); } }
    }
} 