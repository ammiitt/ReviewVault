using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;


namespace ReviewVault.Application.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IUserRepository _userRepo;

        public CommentService(ICommentRepository commentRepo, IUserRepository userRepo)
        {
            _commentRepo = commentRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<CommentResponseDTO>> GetByPostIdAsync(int postId)
        {
            var comments = await _commentRepo.GetByPostIdAsync(postId);
            return comments.Select(c => new CommentResponseDTO
            {
                Id = c.Id,
                Body = c.Body,
                Username = c.Username,
                UserId = c.UserId,
                PostId = c.PostId,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<CommentResponseDTO> CreateAsync(CommentRequestDTO request, int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            var comment = new Comment
            {
                Body = request.Body,
                PostId = request.PostId,
                UserId = userId,
                Username = user.Username,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _commentRepo.CreateAsync(comment);

            return new CommentResponseDTO
            {
                Id = created.Id,
                Body = created.Body,
                Username = created.Username,
                UserId = created.UserId,
                PostId = created.PostId,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task DeleteAsync(int commentId, int userId, bool isAdmin)
        {
            var comment = await _commentRepo.GetByIdAsync(commentId)
                ?? throw new Exception("Comment not found");

            // Only the comment owner or admin can delete
            if (comment.UserId != userId && !isAdmin)
                throw new Exception("You can only delete your own comments");

            await _commentRepo.DeleteAsync(commentId);
        }

        public async Task<int> GetCountAsync(int postId)
        {
            return await _commentRepo.GetCountByPostIdAsync(postId);
        }
    }
}
