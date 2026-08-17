using ReviewVault.Application.DTOs.RequestDTOs;
using ReviewVault.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Interfaces
{
    public interface IUserService
    {

        Task<UserProfileDTO> GetProfileAsync(int userId);
        Task UpdateProfileAsync(int userId, UpdateProfileRequestDTO request);
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDTO request);
    }
}
