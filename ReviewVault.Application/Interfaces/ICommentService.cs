using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDTO>> GetByPostIdAsync(int postId);
        Task<CommentResponseDTO> CreateAsync(CommentRequestDTO request, int userId);
        Task DeleteAsync(int commentId, int userId, bool isAdmin);
        Task<int> GetCountAsync(int postId);
    }
}
