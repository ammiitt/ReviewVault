using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly ILikeRepository _likeRepo;
        private readonly IBookmarkRepository _bookmarkRepo;

        public UserService(
            IUserRepository userRepo,
            ICommentRepository commentRepo,
            ILikeRepository likeRepo,
            IBookmarkRepository bookmarkRepo)
        {
            _userRepo = userRepo;
            _commentRepo = commentRepo;
            _likeRepo = likeRepo;
            _bookmarkRepo = bookmarkRepo;
        }


        public async Task<UserProfileDTO> GetProfileAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            var bookmarks = await _bookmarkRepo.GetByUserIdAsync(userId);
            var likedPostIds = await _likeRepo.GetLikedPostIdsByUserAsync(userId);
            var commentCount = await _commentRepo.GetCountByUserIdAsync(userId);

            return new UserProfileDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl,
                CreatedAt = user.CreatedAt,
                TotalBookmarks = bookmarks.Count(),
                TotalLikes = likedPostIds.Count(),
                TotalComments = commentCount
            };
        }

        public async Task UpdateProfileAsync(int userId, UpdateProfileRequestDTO request)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            // Update only provided fields
            if (request.Username != null) user.Username = request.Username;
            if (request.Bio != null) user.Bio = request.Bio;
            if (request.AvatarUrl != null) user.AvatarUrl = request.AvatarUrl;

            await _userRepo.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDTO request)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect");

            var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userRepo.UpdatePasswordAsync(userId, newHash);
        }
    }
}
