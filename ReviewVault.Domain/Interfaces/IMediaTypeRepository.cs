using ReviewVault.Domain.Models;


namespace ReviewVault.Domain.Interfaces
{
    public interface IMediaTypeRepository
    {
        Task<IEnumerable<MediaType>> GetAllActiveAsync();
        Task<MediaType?> GetByIdAsync(int id);
        Task<MediaType> CreateAsync(MediaType mediaType);
        Task UpdateAsync(MediaType mediaType);
    }
}
