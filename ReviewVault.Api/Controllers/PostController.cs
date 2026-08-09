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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDTO>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var posts = await _postService.GetAllPublishedAsync(page, pageSize);
            var total = await _postService.GetTotalCountAsync();

            return Ok(new
            {
                data = posts,
                totalCount = total,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling(total / (double)pageSize)
            });
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<PostResponseDTO>> GetBySlug(string slug)
        {
            var post = await _postService.GetBySlugAsync(slug);
            return Ok(post);
        }

        // Add this method alongside the existing GetBySlug
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PostResponseDTO>> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return Ok(post);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<PostResponseDTO>>> GetByCategory(
        int categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var posts = await _postService.GetByCategoryAsync(categoryId, page, pageSize);
            return Ok(posts);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PostResponseDTO>> Create(CreateRequestDTO request)
        {
            var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var post = await _postService.CreateAsync(request, authorId);
            return CreatedAtAction(nameof(GetBySlug), new { slug = post.Slug }, post);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PostResponseDTO>> Update(int id, UpdateRequestDTO request)
        {
            var post = await _postService.UpdateAsync(id, request);
            return Ok(post);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeleteAsync(id);
            return NoContent();
        }
    }
}
