using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;

namespace ReviewVault.Application.Service
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepo;

        public LikeService(ILikeRepository likeRepo)
        {
            _likeRepo = likeRepo;
        }

        public async Task<LikeInfoDTO> GetLikeInfoAsync(int postId, int? userId)
        {
            var count = await _likeRepo.GetCountByPostIdAsync(postId);
            var isLiked = userId.HasValue && await _likeRepo.ExistsAsync(userId.Value, postId);

            return new LikeInfoDTO
            {
                Count = count,
                IsLikedByUser = isLiked
            };
        }

        public async Task<LikeInfoDTO> ToggleLikeAsync(int postId, int userId)
        {
            var exists = await _likeRepo.ExistsAsync(userId, postId);

            if (exists)
            {
                await _likeRepo.DeleteAsync(userId, postId);
            }
            else
            {
                await _likeRepo.CreateAsync(new Like
                {
                    UserId = userId,
                    PostId = postId,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return await GetLikeInfoAsync(postId, userId);
        }
    }
}
