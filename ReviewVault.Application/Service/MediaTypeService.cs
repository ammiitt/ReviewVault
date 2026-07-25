using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;


namespace ReviewVault.Application.Service
{
    public class MediaTypeService : IMediaTypeService   
    {
        private readonly IMediaTypeRepository _mediaTypeRepo;

        public MediaTypeService(IMediaTypeRepository mediaTypeRepo)
        {
            _mediaTypeRepo = mediaTypeRepo;
        }

        public async Task<IEnumerable<MediaTypeResponseDTO>> GetAllActiveAsync()
        {
            var mediaTypes = await _mediaTypeRepo.GetAllActiveAsync();
            return mediaTypes.Select(m => new MediaTypeResponseDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                IsActive = m.IsActive
            });
        }

        public async Task<MediaTypeResponseDTO> CreateAsync(string name, string? description)
        {
            var mediaType = new MediaType
            {
                Name = name,
                Description = description,
                IsActive = true
            };

            var created = await _mediaTypeRepo.CreateAsync(mediaType);
            return new MediaTypeResponseDTO
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                IsActive = created.IsActive
            };
        }

        public async Task UpdateAsync(int id, string name, string? description)
        {
            var mediaType = await _mediaTypeRepo.GetByIdAsync(id)
                ?? throw new Exception("Media type not found");

            mediaType.Name = name;
            mediaType.Description = description;

            await _mediaTypeRepo.UpdateAsync(mediaType);
        }

        public async Task DeactivateAsync(int id)
        {
            var mediaType = await _mediaTypeRepo.GetByIdAsync(id)
                ?? throw new Exception("Media type not found");

            mediaType.IsActive = false;
            await _mediaTypeRepo.UpdateAsync(mediaType);
        }
    }
}
