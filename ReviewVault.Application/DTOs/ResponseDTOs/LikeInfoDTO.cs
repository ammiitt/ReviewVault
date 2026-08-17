using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.DTOs.ResponseDTOs
{
    public class LikeInfoDTO
    {
        public int Count { get; set; }
        public bool IsLikedByUser { get; set; }
    }
}
