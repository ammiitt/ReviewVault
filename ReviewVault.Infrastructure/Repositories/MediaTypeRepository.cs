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
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly AppDbContext _context;

        public MediaTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MediaType>> GetAllActiveAsync()
        {
            var entities = await _context.MediaTypes
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name)
                .AsNoTracking()
                .ToListAsync();
            return entities.Select(e => e.ToDomain());
        }

        public async Task<MediaType?> GetByIdAsync(int id)
        {
            var entity = await _context.MediaTypes.FindAsync(id);
            return entity?.ToDomain();
        }

        public async Task<MediaType> CreateAsync(MediaType mediaType)
        {
            var entity = mediaType.ToEntity();
            _context.MediaTypes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDomain();
        }

        public async Task UpdateAsync(MediaType mediaType)
        {
            var entity = await _context.MediaTypes.FindAsync(mediaType.Id);
            if (entity != null)
            {
                entity.Name = mediaType.Name;
                entity.Description = mediaType.Description;
                entity.IsActive = mediaType.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
