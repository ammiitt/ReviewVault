using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface IMediaTypeService
    {
        Task<IEnumerable<MediaTypeResponseDTO>> GetAllActiveAsync();
        Task<MediaTypeResponseDTO> CreateAsync(string name, string? description);
        Task UpdateAsync(int id, string name, string? description);
        Task DeactivateAsync(int id);
    }
}
