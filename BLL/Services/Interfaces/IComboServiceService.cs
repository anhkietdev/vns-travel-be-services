using BLL.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IComboServiceService
    {
        Task<IEnumerable<ComboServiceDto>> GetComboServicesByComboIdAsync(Guid comboId);
        Task AddServiceToComboAsync(Guid comboId, Guid serviceId);
        Task RemoveServiceFromComboAsync(Guid comboId, Guid serviceId);
    }
}
