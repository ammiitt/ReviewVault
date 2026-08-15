using ReviewVault.Domain.Enums;
using ReviewVault.Domain.Models;
using ReviewVault.Infrastructure.Entities;


namespace ReviewVault.Infrastructure.Mappings
{
    public static class EntityMappings
    {
        public static User ToDomain(this UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                PasswordHash = entity.PasswordHash,
                Role = entity.Role,
                Bio = entity.Bio,
                AvatarUrl = entity.AvatarUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static UserEntity ToEntity(this User model)
        {
            return new UserEntity
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                Role = model.Role,
                Bio = model.Bio,
                AvatarUrl = model.AvatarUrl,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        // ═══ POST ═══
        public static Post ToDomain(this PostEntity entity)
        {
            return new Post
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                Body = entity.Body,
                Summary = entity.Summary,
                CoverImageUrl = entity.CoverImageUrl,
                Rating = (Rating)entity.Rating,
                IsPublished = entity.IsPublished,
                PublishedAt = entity.PublishedAt,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                AuthorId = entity.AuthorId,
                MediaTypeId = entity.MediaTypeId,
                AuthorName = entity.Author?.Username ?? string.Empty,
                MediaTypeName = entity.MediaType?.Name ?? string.Empty,
                Categories = entity.Categories?.Select(c => c.Name).ToList() ?? new List<string>(),
                CategoryIds = entity.Categories?.Select(c => c.Id).ToList() ?? new List<int>(),
            };
        }

        public static PostEntity ToEntity(this Post model)
        {
            return new PostEntity
            {
                Id = model.Id,
                Title = model.Title,
                Slug = model.Slug,
                Body = model.Body,
                Summary = model.Summary,
                CoverImageUrl = model.CoverImageUrl,
                Rating = (int)model.Rating,
                IsPublished = model.IsPublished,
                PublishedAt = model.PublishedAt,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                AuthorId = model.AuthorId,
                MediaTypeId = model.MediaTypeId
            };
        }

        // ═══ CATEGORY ═══
        public static Category ToDomain(this CategoryEntity entity)
        {
            return new Category
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }

        public static CategoryEntity ToEntity(this Category model)
        {
            return new CategoryEntity
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = model.CreatedAt
            };
        }

        // ═══ MEDIA TYPE ═══
        public static MediaType ToDomain(this MediaTypeEntity entity)
        {
            return new MediaType
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static MediaTypeEntity ToEntity(this MediaType model)
        {
            return new MediaTypeEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive
            };
        }

        // ═══ REFRESH TOKEN ═══
        public static RefreshToken ToDomain(this RefreshTokenEntity entity)
        {
            return new RefreshToken
            {
                Id = entity.Id,
                Token = entity.Token,
                UserId = entity.UserId,
                ExpiresAt = entity.ExpiresAt,
                CreatedAt = entity.CreatedAt,
                RevokedAt = entity.RevokedAt
            };
        }

        public static RefreshTokenEntity ToEntity(this RefreshToken model)
        {
            return new RefreshTokenEntity
            {
                Id = model.Id,
                Token = model.Token,
                UserId = model.UserId,
                ExpiresAt = model.ExpiresAt,
                CreatedAt = model.CreatedAt,
                RevokedAt = model.RevokedAt
            };
        }

        // ═══ COMMENT ═══
        public static Comment ToDomain(this CommentEntity entity)
        {
            return new Comment
            {
                Id = entity.Id,
                Body = entity.Body,
                PostId = entity.PostId,
                UserId = entity.UserId,
                Username = entity.User?.Username ?? string.Empty,
                CreatedAt = entity.CreatedAt
            };
        }

        public static CommentEntity ToEntity(this Comment model)
        {
            return new CommentEntity
            {
                Id = model.Id,
                Body = model.Body,
                PostId = model.PostId,
                UserId = model.UserId,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
