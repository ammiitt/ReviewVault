using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;


namespace ReviewVault.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryResponseDTO> CreateAsync(string name)
        {
            var category = new Category { Name = name, CreatedAt = DateTime.UtcNow };
            var created = await _categoryRepo.CreateAsync(category);
            return new CategoryResponseDTO
            {
                Id = created.Id,
                Name = created.Name
            };
        }
    }
}
