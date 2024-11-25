using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IMS.Domain.ViewModels;

namespace IMS.Application.ServiceInterface
{
    public interface IAdministrator : IBaseInterface<object>
    {
        Task<bool> CreateUserAsync(RegisterVM data, IFormFile file);
        Task<bool> UpdateUserAsync(RegisterVM data, IFormFile file);
        Task<bool> DeleteUsers(string ListOfId);

        Task<dynamic> GetDropdownData(int flag);

        Task<(string token, string refreshToken)> GenerateJwtToken(string userId, string roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<RefreshTokenVM> GetSavedRefreshToken(string token);    
        Task<dynamic> UpdateRefreshToken(RefreshTokenVM data);
        Task<dynamic> SaveMenu();
        Task<dynamic> MenuPermissionData(int menuId, string roleId);
       
    }
}
