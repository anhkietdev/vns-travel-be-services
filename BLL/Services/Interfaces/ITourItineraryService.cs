using BLL.DTOs.Tour;
using DAL.Models;

namespace BLL.Services.Interfaces
{
    internal interface ITourItineraryService
    {
        Task<ICollection<TourItinerary>> GetAllAsync();
        Task<TourItinerary?> GetByIdAsync(Guid id);
        Task AddAsync(CreateTourItineraryDto entity);
        Task UpdateAsync(CreateTourItineraryDto entity);
        Task DeleteAsync(Guid id);
    }
}
