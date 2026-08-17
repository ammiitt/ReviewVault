using Microsoft.EntityFrameworkCore;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;
using ReviewVault.Infrastructure.Data;
using ReviewVault.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly AppDbContext _context;

        public BookmarkRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int postId)
        {
            return await _context.Bookmarks.AnyAsync(b => b.UserId == userId && b.PostId == postId);
        }

        public async Task<Bookmark> CreateAsync(Bookmark bookmark)
        {
            var entity = bookmark.ToEntity();
            _context.Bookmarks.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task DeleteAsync(int userId, int postId)
        {
            var entity = await _context.Bookmarks
                .FirstOrDefaultAsync(b => b.UserId == userId && b.PostId == postId);
            if (entity != null)
            {
                _context.Bookmarks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Bookmark>> GetByUserIdAsync(int userId)
        {
            var entities = await _context.Bookmarks
                .Include(b => b.Post)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(e => e.ToDomain());
        }

    }
}
