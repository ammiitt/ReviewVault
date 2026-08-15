using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using System.Security.Claims;

namespace ReviewVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // Public — anyone can read comments
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentResponseDTO>>> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        // Protected — logged in users can comment
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentResponseDTO>> Create(CommentRequestDTO request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var comment = await _commentService.CreateAsync(request, userId);
            return CreatedAtAction(nameof(GetByPostId), new { postId = comment.PostId }, comment);
        }

        // Protected — owner or admin can delete
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            await _commentService.DeleteAsync(id, userId, isAdmin);
            return NoContent();
        }

        // Public — get comment count for a post
        [HttpGet("post/{postId}/count")]
        public async Task<ActionResult<int>> GetCount(int postId)
        {
            var count = await _commentService.GetCountAsync(postId);
            return Ok(count);
        }
    }
}
