using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Service
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepo;

        public BookmarkService(IBookmarkRepository bookmarkRepo)
        {
            _bookmarkRepo = bookmarkRepo;
        }

        public async Task<bool> IsBookmarkedAsync(int postId, int userId)
        {
            return await _bookmarkRepo.ExistsAsync(userId, postId);
        }

        public async Task<bool> ToggleBookmarkAsync(int postId, int userId)
        {
            var exists = await _bookmarkRepo.ExistsAsync(userId, postId);

            if (exists)
            {
                await _bookmarkRepo.DeleteAsync(userId, postId);
                return false;
            }
            else
            {
                await _bookmarkRepo.CreateAsync(new Bookmark
                {
                    UserId = userId,
                    PostId = postId,
                    CreatedAt = DateTime.UtcNow
                });
                return true;
            }
        }

        public async Task<IEnumerable<BookmarkResponseDTO>> GetUserBookmarksAsync(int userId)
        {
            var bookmarks = await _bookmarkRepo.GetByUserIdAsync(userId);
            return bookmarks.Select(b => new BookmarkResponseDTO
            {
                Id = b.Id,
                PostId = b.PostId,
                PostTitle = b.PostTitle,
                PostSlug = b.PostSlug,
                PostCoverImageUrl = b.PostCoverImageUrl,
                CreatedAt = b.CreatedAt
            });
        }
    }
}
