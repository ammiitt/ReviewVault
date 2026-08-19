using AutoMapper;
using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Application.Interfaces;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;


namespace ReviewVault.Application.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepo, ICategoryRepository categoryRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<PostResponseDTO> GetByIdAsync(int id)
        {
            var post = await _postRepo.GetByIdAsync(id)
                ?? throw new Exception("Post not found");
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task<PostResponseDTO> GetBySlugAsync(string slug)
        {
            var post = await _postRepo.GetBySlugAsync(slug)
                ?? throw new Exception("Post not found");
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task<IEnumerable<PostResponseDTO>> GetByCategoryAsync(int categoryId, int page, int pageSize)
        {
            var posts = await _postRepo.GetByCategoryAsync(categoryId, page, pageSize);
            return _mapper.Map<IEnumerable<PostResponseDTO>>(posts);
        }

        public async Task<IEnumerable<PostResponseDTO>> GetAllPublishedAsync(int page, int pageSize)
        {
            var posts = await _postRepo.GetAllPublishedAsync(page, pageSize);
            return _mapper.Map<IEnumerable<PostResponseDTO>>(posts);
        }

        public async Task<IEnumerable<PostResponseDTO>> SearchAsync(string q,int page, int pageSize)
        {
            var posts = await _postRepo.SearchAsync(q, page, pageSize);
            return _mapper.Map<IEnumerable<PostResponseDTO>>( posts);
        }

        public async Task<int> SearchCountAsync(string query)
        {
            return await _postRepo.SearchCountAsync(query);
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _postRepo.GetTotalCountAsync(true);
        }

        public async Task<PostResponseDTO> CreateAsync(CreateRequestDTO request, int authorId)
        {
            // AutoMapper handles basic fields, we set the rest
            var post = _mapper.Map<Post>(request);
            post.Slug = GenerateSlug(request.Title);
            post.AuthorId = authorId;
            post.PublishedAt = request.IsPublished ? DateTime.UtcNow : null;
            post.CreatedAt = DateTime.UtcNow;

            if (request.CategoryIds.Any())
            {
                var categories = await _categoryRepo.GetByIdsAsync(request.CategoryIds);
                post.Categories = categories.Select(c => c.Name).ToList();
            }

            var created = await _postRepo.CreateAsync(post);
            return _mapper.Map<PostResponseDTO>(created);
        }

        public async Task<PostResponseDTO> UpdateAsync(int id, UpdateRequestDTO request)
        {
            var post = await _postRepo.GetByIdAsync(id)
                ?? throw new Exception("Post not found");

            // AutoMapper updates existing object from request
            _mapper.Map(request, post);
            post.Slug = GenerateSlug(request.Title);
            post.UpdatedAt = DateTime.UtcNow;

            if (request.IsPublished && post.PublishedAt == null)
                post.PublishedAt = DateTime.UtcNow;

            if (request.CategoryIds.Any())
            {
                var categories = await _categoryRepo.GetByIdsAsync(request.CategoryIds);
                post.Categories = categories.Select(c => c.Name).ToList();
            }

            await _postRepo.UpdateAsync(post);
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task DeleteAsync(int id)
        {
            await _postRepo.DeleteAsync(id);
        }

        private static string GenerateSlug(string title)
        {
            return title.ToLower()
                .Replace(" ", "-")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace(".", "")
                .Replace(",", "");
        }
    }
}
