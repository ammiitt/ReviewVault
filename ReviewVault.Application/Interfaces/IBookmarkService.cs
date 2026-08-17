using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface IBookmarkService
    {
        Task<bool> IsBookmarkedAsync(int postId, int userId);
        Task<bool> ToggleBookmarkAsync(int postId, int userId);
        Task<IEnumerable<BookmarkResponseDTO>> GetUserBookmarksAsync(int userId);
    }
}
