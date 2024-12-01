using IMS.Domain.Models;
using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IRole : IBaseInterface<AspNetRole>
    {
        Task<bool>CreateOrUpdateRole(AspNetRoleVM role);
        Task<dynamic>GetAllRoleAsync();
        Task<dynamic> GetRoleByRoleId(string roleId);
    }
}
