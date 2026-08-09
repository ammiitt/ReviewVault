using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Entities
{
    public class PostEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Rating { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }

        // Foreign keys
        public int AuthorId { get; set; }
        public int MediaTypeId { get; set; }

        // Navigation
        public UserEntity Author { get; set; } = null!;
        public MediaTypeEntity MediaType { get; set; } = null!;
        public ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

    }
}
