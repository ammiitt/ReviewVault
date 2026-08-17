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
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<LikeInfoDTO>> GetLikeInfo(int postId)
        {
            int? userId = null;
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) userId = int.Parse(claim.Value);

            var info = await _likeService.GetLikeInfoAsync(postId, userId);
            return Ok(info);
        }

        [Authorize]
        [HttpPost("toggle/{postId}")]
        public async Task<ActionResult<LikeInfoDTO>> ToggleLike(int postId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var info = await _likeService.ToggleLikeAsync(postId, userId);
            return Ok(info);
        }
    }
}
