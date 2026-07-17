using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
    }
}
