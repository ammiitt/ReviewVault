using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Entities
{
    public class Category
    {
        public string Name { get; set; } = string.Empty;  // Action, Romance, Shounen, etc.

        // Navigation (Many-to-Many)
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
