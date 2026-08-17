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
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int postId)
        {
            return await _context.Likes.AnyAsync(l => l.UserId == userId && l.PostId == postId);
        }

        public async Task<Like> CreateAsync(Like like)
        {
            var entity = like.ToEntity();
            _context.Likes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task DeleteAsync(int userId, int postId)
        {
            var entity = await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
            if (entity != null)
            {
                _context.Likes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountByPostIdAsync(int postId)
        {
            return await _context.Likes.CountAsync(l => l.PostId == postId);
        }

        public async Task<IEnumerable<int>> GetLikedPostIdsByUserAsync(int userId)
        {
            return await _context.Likes
                .Where(l => l.UserId == userId)
                .Select(l => l.PostId)
                .ToListAsync();
        }

    }
}
