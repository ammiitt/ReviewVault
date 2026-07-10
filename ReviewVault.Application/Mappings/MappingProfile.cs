using AutoMapper;
using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Domain.Enums;
using ReviewVault.Domain.Models;


namespace ReviewVault.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ═══ POST MAPPINGS ═══

            // Post (Domain) → PostResponseDTO
            CreateMap<Post, PostResponseDTO>()
                .ForMember(d => d.Rating, o => o.MapFrom(s => (int)s.Rating))
                .ForMember(d => d.RatingName, o => o.MapFrom(s => s.Rating.ToString()));

            // CreatePostRequestDTO → Post (Domain)
            CreateMap<CreateRequestDTO, Post>()
                .ForMember(d => d.Rating, o => o.MapFrom(s => (Rating)s.Rating))
                .ForMember(d => d.Slug, o => o.Ignore())          // generated in service
                .ForMember(d => d.Categories, o => o.Ignore())     // handled in service
                .ForMember(d => d.AuthorId, o => o.Ignore())       // set in service
                .ForMember(d => d.AuthorName, o => o.Ignore())
                .ForMember(d => d.MediaTypeName, o => o.Ignore())
                .ForMember(d => d.PublishedAt, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            // UpdatePostRequestDTO → Post (Domain)
            CreateMap<UpdateRequestDTO, Post>()
                .ForMember(d => d.Rating, o => o.MapFrom(s => (Rating)s.Rating))
                .ForMember(d => d.Slug, o => o.Ignore())
                .ForMember(d => d.Categories, o => o.Ignore())
                .ForMember(d => d.AuthorId, o => o.Ignore())
                .ForMember(d => d.AuthorName, o => o.Ignore())
                .ForMember(d => d.MediaTypeName, o => o.Ignore())
                .ForMember(d => d.PublishedAt, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            
            // ═══ USER MAPPINGS ═══

            // RegisterRequestDTO → User (Domain)
            CreateMap<RegisterRequestDTO, User>()
                .ForMember(d => d.PasswordHash, o => o.Ignore())   // hashed in service
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Role, o => o.Ignore())
                .ForMember(d => d.Bio, o => o.Ignore())
                .ForMember(d => d.AvatarUrl, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());

            // User (Domain) → AuthResponseDTO (partial — token fields set in service)
            CreateMap<User, AuthResponseDTO>()
                .ForMember(d => d.AccessToken, o => o.Ignore())
                .ForMember(d => d.RefreshToken, o => o.Ignore())
                .ForMember(d => d.AccessTokenExpiresAt, o => o.Ignore())
                .ForMember(d => d.RefreshTokenExpiresAt, o => o.Ignore());
        }

    }
}
