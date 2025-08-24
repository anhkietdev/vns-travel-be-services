using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implementations
{

    public class ComboService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComboService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboDto>> GetAllCombosAsync()
        {
            var combos = await _unitOfWork.Combo.GetAllAsync();
            return _mapper.Map<IEnumerable<ComboDto>>(combos);
        }

        public async Task<ComboDto?> GetComboByIdAsync(Guid comboId)
        {
            var combo = await _unitOfWork.Combo.GetAsync(c => c.ComboId == comboId);
            return combo == null ? null : _mapper.Map<ComboDto>(combo);
        }

        public async Task<ComboDto> CreateComboAsync(ComboDto comboDto)
        {
            var combo = _mapper.Map<Combo>(comboDto);
            await _unitOfWork.Combo.AddAsync(combo);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ComboDto>(combo);
        }

        public async Task<bool> DeleteComboAsync(Guid comboId)
        {
            var combo = await _unitOfWork.Combo.GetAsync(c => c.ComboId == comboId);
            if (combo == null) return false;
            await _unitOfWork.Combo.RemoveAsync(combo);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
