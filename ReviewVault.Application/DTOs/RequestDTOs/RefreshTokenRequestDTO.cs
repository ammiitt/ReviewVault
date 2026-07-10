using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.RequestDTOs
{
    public class RefreshTokenRequestDTO
    {
        public string Token { get; set; } = string.Empty;
    }
}
