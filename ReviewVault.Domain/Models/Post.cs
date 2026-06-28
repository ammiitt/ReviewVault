using ReviewVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? CoverImageUrl { get; set; }
        public Rating Rating { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Just IDs — no navigation properties
        public int AuthorId { get; set; }
        public int MediaTypeId { get; set; }

        // Simple data — not EF collections
        public string AuthorName { get; set; } = string.Empty;
        public string MediaTypeName { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();
    }
}
