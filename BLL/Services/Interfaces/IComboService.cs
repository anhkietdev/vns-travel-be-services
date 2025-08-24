using BLL.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IComboService
    {
        Task<IEnumerable<ComboDto>> GetAllCombosAsync();
        Task<ComboDto?> GetComboByIdAsync(Guid comboId);
        Task<ComboDto> CreateComboAsync(ComboDto comboDto);
        Task<bool> DeleteComboAsync(Guid comboId);        
    }
}
