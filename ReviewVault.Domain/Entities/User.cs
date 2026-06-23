using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Admin";                // Admin (you), Reader (future)
        public string? Bio { get; set; }                            // About the author
        public string? AvatarUrl { get; set; }

        // Navigation
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
