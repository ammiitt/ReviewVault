using ReviewVault.Domain.Models;

namespace ReviewVault.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
