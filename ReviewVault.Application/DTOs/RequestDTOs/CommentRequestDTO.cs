using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.RequestDTOs
{
    public class CommentRequestDTO
    {
        public string Body { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
