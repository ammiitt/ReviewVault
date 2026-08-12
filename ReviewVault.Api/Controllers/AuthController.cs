using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;

namespace ReviewVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterRequestDTO request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<ActionResult<AuthResponseDTO>> RegisterAdmin(RegisterRequestDTO request)
        {
            var response = await _authService.RegisterAdminAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRequestDTO request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDTO>> Refresh(RefreshTokenRequestDTO request)
        {
            var response = await _authService.RefreshTokenAsync(request);
            return Ok(response);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(RefreshTokenRequestDTO request)
        {
            await _authService.RevokeTokenAsync(request.Token);
            return Ok(new { message = "Token revoked successfully" });
        }
    }
}
