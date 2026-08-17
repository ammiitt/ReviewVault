using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.RequestDTOs
{
    public class UpdateProfileRequestDTO
    {
        public string? Username { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
