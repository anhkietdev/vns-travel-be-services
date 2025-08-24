using BLL.DTOs.Tour;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implementations
{
    public class TourItineraryService : ITourItineraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TourItineraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(CreateTourItineraryDto entity)
        {
            var tourItinerary = new TourItinerary
            {
                ItineraryId = Guid.NewGuid(),
                TourId = entity.TourId,
                StepOrder = entity.StepOrder,
                Location = entity.Location,
                Activity = entity.Activity,
                DurationMinutes = entity.DurationMinutes,
                Description = entity.Description,
            };
            await _unitOfWork.TourItinerary.AddAsync(tourItinerary);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TourItinerary>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TourItinerary?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CreateTourItineraryDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
