using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllAsync();
        Task<CategoryResponseDTO> CreateAsync(string name);
    }
}
