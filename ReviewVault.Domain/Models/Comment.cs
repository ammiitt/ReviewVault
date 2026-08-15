using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
