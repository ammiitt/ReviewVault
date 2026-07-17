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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var entities = await _context.Categories
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync();
            return entities.Select(e => e.ToDomain());
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            return entity?.ToDomain();
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(List<int> ids)
        {
            var entities = await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
            return entities.Select(e => e.ToDomain());
        }

        public async Task<Category> CreateAsync(Category category)
        {
            var entity = category.ToEntity();
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task UpdateAsync(Category category)
        {
            var entity = await _context.Categories.FindAsync(category.Id);
            if (entity != null)
            {
                entity.Name = category.Name;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
