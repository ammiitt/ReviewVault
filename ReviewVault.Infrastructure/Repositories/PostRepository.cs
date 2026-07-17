using Microsoft.EntityFrameworkCore;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;
using ReviewVault.Infrastructure.Data;
using ReviewVault.Infrastructure.Entities;
using ReviewVault.Infrastructure.Mappings;


namespace ReviewVault.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<PostEntity> FullQuery()
        {
            return _context.Posts
                .Include(p => p.Author)
                .Include(p => p.MediaType)
                .Include(p => p.Categories);
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var entity = await FullQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
            return entity?.ToDomain();
        }

        public async Task<Post?> GetBySlugAsync(string slug)
        {
            var entity = await FullQuery()
                .FirstOrDefaultAsync(p => p.Slug == slug);
            return entity?.ToDomain();
        }

        public async Task<IEnumerable<Post>> GetAllPublishedAsync(int page, int pageSize)
        {
            var entities = await FullQuery()
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(e => e.ToDomain());
        }

        public async Task<IEnumerable<Post>> GetByMediaTypeAsync(int mediaTypeId, int page, int pageSize)
        {
            var entities = await FullQuery()
                .Where(p => p.IsPublished && p.MediaTypeId == mediaTypeId)
                .OrderByDescending(p => p.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(e => e.ToDomain());
        }

        public async Task<IEnumerable<Post>> GetByCategoryAsync(int categoryId, int page, int pageSize)
        {
            var entities = await FullQuery()
                .Where(p => p.IsPublished && p.Categories.Any(c => c.Id == categoryId))
                .OrderByDescending(p => p.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(e => e.ToDomain());
        }

        public async Task<int> GetTotalCountAsync(bool publishedOnly = true)
        {
            return publishedOnly
                ? await _context.Posts.CountAsync(p => p.IsPublished)
                : await _context.Posts.CountAsync();
        }

        public async Task<Post> CreateAsync(Post post)
        {
            var entity = post.ToEntity();

            // Handle many-to-many: find category entities by name
            if (post.Categories.Any())
            {
                var categoryEntities = await _context.Categories
                    .Where(c => post.Categories.Contains(c.Name))
                    .ToListAsync();
                entity.Categories = categoryEntities;
            }

            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with all includes for complete domain model
            return (await GetByIdAsync(entity.Id))!;
        }

        public async Task UpdateAsync(Post post)
        {
            var entity = await _context.Posts
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == post.Id);

            if (entity == null) return;

            entity.Title = post.Title;
            entity.Slug = post.Slug;
            entity.Body = post.Body;
            entity.Summary = post.Summary;
            entity.CoverImageUrl = post.CoverImageUrl;
            entity.Rating = (int)post.Rating;
            entity.MediaTypeId = post.MediaTypeId;
            entity.IsPublished = post.IsPublished;
            entity.PublishedAt = post.PublishedAt;
            entity.UpdatedAt = DateTime.UtcNow;

            // Clear and reassign categories
            entity.Categories.Clear();
            if (post.Categories.Any())
            {
                var categoryEntities = await _context.Categories
                    .Where(c => post.Categories.Contains(c.Name))
                    .ToListAsync();
                entity.Categories = categoryEntities;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Posts.FindAsync(id);
            if (entity != null)
            {
                _context.Posts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
