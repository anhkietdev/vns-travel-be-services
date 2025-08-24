using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implementations
{
    public class TourServiceService : ITourService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TourServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<TourService>> GetAllAsync() => await _unitOfWork.TourService.GetAllAsync();
        public async Task<TourService?> GetByIdAsync(Guid id) => await _unitOfWork.TourService.GetAsync(x => x.TourId == id);
        public async Task AddAsync(TourService entity) { await _unitOfWork.TourService.AddAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task UpdateAsync(TourService entity) { await _unitOfWork.TourService.UpdateAsync(entity); await _unitOfWork.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var entity = await _unitOfWork.TourService.GetAsync(x => x.TourId == id); if (entity != null) { await _unitOfWork.TourService.RemoveAsync(entity); await _unitOfWork.SaveChangesAsync(); } }
    }
} 