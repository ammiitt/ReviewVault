using Microsoft.EntityFrameworkCore;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;
using ReviewVault.Infrastructure.Data;
using ReviewVault.Infrastructure.Mappings;


namespace ReviewVault.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            var entities = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(e => e.ToDomain());
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var entity = await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            return entity?.ToDomain();
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            var entity = comment.ToEntity();
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with User include for username
            var created = await _context.Comments
                .Include(c => c.User)
                .FirstAsync(c => c.Id == entity.Id);

            return created.ToDomain();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity != null)
            {
                _context.Comments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountByPostIdAsync(int postId)
        {
            return await _context.Comments.CountAsync(c => c.PostId == postId);
        }
    }
}
