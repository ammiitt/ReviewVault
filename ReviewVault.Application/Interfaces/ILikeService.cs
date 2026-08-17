using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface ILikeService
    {
        Task<LikeInfoDTO> GetLikeInfoAsync(int postId, int? userId);
        Task<LikeInfoDTO> ToggleLikeAsync(int postId, int userId);
    }
}
