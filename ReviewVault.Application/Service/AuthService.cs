using AutoMapper;
using ReviewVault.Application.DTOs.RequestDTOs;
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
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepo, IJwtService jwtService, IMapper mapper)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            if (await _userRepo.ExistsAsync(request.Email))
                throw new Exception("Email already registered");

            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Role = "User";
            user.CreatedAt = DateTime.UtcNow;

            var createdUser = await _userRepo.CreateAsync(user);
            return await GenerateAuthResponse(createdUser);
        }

        public async Task<AuthResponseDTO> RegisterAdminAsync(RegisterRequestDTO request)
        {
            if (await _userRepo.ExistsAsync(request.Email))
                throw new Exception("Email already registered");

            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Role = "Admin";    // ← Admin role
            user.CreatedAt = DateTime.UtcNow;

            var createdUser = await _userRepo.CreateAsync(user);
            return await GenerateAuthResponse(createdUser);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email)
                ?? throw new Exception("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            var existingToken = await _userRepo.GetRefreshTokenAsync(request.Token)
                ?? throw new Exception("Invalid refresh token");

            if (!existingToken.IsActive)
                throw new Exception("Token is expired or revoked");

            await _userRepo.RevokeRefreshTokenAsync(request.Token);

            var user = await _userRepo.GetByIdAsync(existingToken.UserId)
                ?? throw new Exception("User not found");

            return await GenerateAuthResponse(user);
        }

        public async Task RevokeTokenAsync(string token)
        {
            await _userRepo.RevokeRefreshTokenAsync(token);
        }

        private async Task<AuthResponseDTO> GenerateAuthResponse(User user)
        {
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshTokenString = _jwtService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepo.CreateRefreshTokenAsync(refreshToken);

            // AutoMapper maps User → AuthResponseDTO (Username, Role)
            // Then we set token fields manually (AutoMapper ignores them)
            var response = _mapper.Map<AuthResponseDTO>(user);
            response.AccessToken = accessToken;
            response.RefreshToken = refreshTokenString;
            response.AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(30);
            response.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

            return response;
        }
    }
}