using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Infrastructure.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string Body { get; set; } = string.Empty;

        // Foreign keys
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation
        public PostEntity Post { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
    }
}
