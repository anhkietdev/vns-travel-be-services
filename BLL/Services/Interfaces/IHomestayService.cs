using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IHomestayService
    {
        Task<ICollection<HomestayService>> GetAllAsync();
        Task<HomestayService?> GetByIdAsync(Guid id);
        Task AddAsync(HomestayService entity);
        Task UpdateAsync(HomestayService entity);
        Task DeleteAsync(Guid id);
    }
} 