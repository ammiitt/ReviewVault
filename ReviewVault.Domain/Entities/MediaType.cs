using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Domain.Entities
{
    public class MediaType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;         
        public string? Description { get; set; }                  
        public bool IsActive { get; set; } = true;
    }
}
