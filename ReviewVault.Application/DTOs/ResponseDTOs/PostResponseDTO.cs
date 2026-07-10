using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.ResponseDTOs
{
    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Rating { get; set; }
        public string RatingName { get; set; } = string.Empty;
        public string MediaTypeName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Categories { get; set; } = new();
    }
}
