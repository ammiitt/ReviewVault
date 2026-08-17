using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.ResponseDTOs
{
    public class BookmarkResponseDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostSlug { get; set; } = string.Empty;
        public string? PostCoverImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
