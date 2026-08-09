using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;


namespace ReviewVault.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostResponseDTO> GetByIdAsync(int id);
        Task<PostResponseDTO> GetBySlugAsync(string slug);
        Task<IEnumerable<PostResponseDTO>> GetByCategoryAsync(int categoryId, int page, int pageSize);
        Task<IEnumerable<PostResponseDTO>> GetAllPublishedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<PostResponseDTO> CreateAsync(CreateRequestDTO request, int authorId);
        Task<PostResponseDTO> UpdateAsync(int id, UpdateRequestDTO request);
        Task DeleteAsync(int id);
    }
}
