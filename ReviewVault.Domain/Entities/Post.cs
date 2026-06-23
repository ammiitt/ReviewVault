using ReviewVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = string.Empty;          // "Why Interstellar Hits Different"
        public string Slug { get; set; } = string.Empty;           // "why-interstellar-hits-different" (URL friendly)
        public string Body { get; set; } = string.Empty;            // Full blog content (HTML/Markdown)
        public string? Summary { get; set; }                        // Short preview text
        public string? CoverImageUrl { get; set; }                  // Banner image
        public Rating Rating { get; set; }                          // Your personal rating
        public bool IsPublished { get; set; } = false;              // Draft or Live
        public DateTime? PublishedAt { get; set; }                  // When it went live

        // Foreign keys
        public int AuthorId { get; set; }
        public int MediaTypeId { get; set; }

        // Navigation
        public User Author { get; set; } = null!;
        public MediaType MediaType { get; set; } = null!;
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
