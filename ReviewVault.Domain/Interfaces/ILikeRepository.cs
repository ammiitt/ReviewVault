using ReviewVault.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Interfaces
{
    public interface ILikeRepository
    {
        Task<bool> ExistsAsync(int userId, int postId);
        Task<Like> CreateAsync(Like like);
        Task DeleteAsync(int userId, int postId);
        Task<int> GetCountByPostIdAsync(int postId);
        Task<IEnumerable<int>> GetLikedPostIdsByUserAsync(int userId);
    }
}
