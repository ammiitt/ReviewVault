using ReviewVault.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<int> GetCountByPostIdAsync(int postId);

        Task<int> GetCountByUserIdAsync(int userId);
    }
}
