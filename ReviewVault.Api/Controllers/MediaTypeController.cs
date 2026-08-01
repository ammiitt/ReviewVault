using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;

namespace ReviewVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaTypeController : ControllerBase
    {
        private readonly IMediaTypeService _mediaTypeService;

        public MediaTypeController(IMediaTypeService mediaTypeService)
        {
            _mediaTypeService = mediaTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaTypeResponseDTO>>> GetAll()
        {
            var mediaTypes = await _mediaTypeService.GetAllActiveAsync();
            return Ok(mediaTypes);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MediaTypeResponseDTO>> Create(
            [FromQuery] string name,
            [FromQuery] string? description)
        {
            var mediaType = await _mediaTypeService.CreateAsync(name, description);
            return CreatedAtAction(nameof(GetAll), mediaType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromQuery] string name,
            [FromQuery] string? description)
        {
            await _mediaTypeService.UpdateAsync(id, name, description);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _mediaTypeService.DeactivateAsync(id);
            return NoContent();
        }
    }
}
