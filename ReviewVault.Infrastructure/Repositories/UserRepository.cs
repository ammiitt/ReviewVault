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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            return entity?.ToDomain();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return entity?.ToDomain();
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            var entity = user.ToEntity();
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken token)
        {
            var entity = token.ToEntity();
            _context.RefreshTokens.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            var entity = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token);
            return entity?.ToDomain();
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var entity = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token);

            if (entity != null)
            {
                entity.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
