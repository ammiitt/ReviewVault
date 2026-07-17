using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Entities
{
    public class MediaTypeEntity : BaseEntity   
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
    }
}
