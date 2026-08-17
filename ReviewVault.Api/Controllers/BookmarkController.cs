using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using System.Security.Claims;

namespace ReviewVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookmarkResponseDTO>>> GetMyBookmarks()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var bookmarks = await _bookmarkService.GetUserBookmarksAsync(userId);
            return Ok(bookmarks);
        }

        [HttpGet("check/{postId}")]
        public async Task<ActionResult<bool>> IsBookmarked(int postId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _bookmarkService.IsBookmarkedAsync(postId, userId);
            return Ok(result);
        }

        [HttpPost("toggle/{postId}")]
        public async Task<ActionResult<bool>> ToggleBookmark(int postId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isBookmarked = await _bookmarkService.ToggleBookmarkAsync(postId, userId);
            return Ok(isBookmarked);
        }
    }
}
