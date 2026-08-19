using ReviewVault.Domain.Models;


namespace ReviewVault.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(int id);
        Task<Post?> GetBySlugAsync(string slug);
        Task<IEnumerable<Post>> GetAllPublishedAsync(int page, int pageSize);
        Task<IEnumerable<Post>> GetByMediaTypeAsync(int mediaTypeId, int page, int pageSize);
        Task<IEnumerable<Post>> GetByCategoryAsync(int categoryId, int page, int pageSize);
        Task<IEnumerable<Post>> SearchAsync(string query, int page, int pageSize);
        Task<int> SearchCountAsync(string query);
        Task<int> GetTotalCountAsync(bool publishedOnly = true);
        Task<Post> CreateAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
