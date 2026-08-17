using ReviewVault.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<bool> ExistsAsync(int userId, int postId);
        Task<Bookmark> CreateAsync(Bookmark bookmark);
        Task DeleteAsync(int userId, int postId);
        Task<IEnumerable<Bookmark>> GetByUserIdAsync(int userId);
    }
}
