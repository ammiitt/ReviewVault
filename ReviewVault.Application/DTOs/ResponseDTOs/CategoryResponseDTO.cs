using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.ResponseDTOs
{
    public class CategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
