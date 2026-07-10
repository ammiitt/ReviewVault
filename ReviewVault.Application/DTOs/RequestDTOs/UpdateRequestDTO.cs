using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.RequestDTOs
{
    public class UpdateRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Rating { get; set; }
        public int MediaTypeId { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public bool IsPublished { get; set; }
    }
}
