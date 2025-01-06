using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Domain.Models;
using IMS.Domain.ViewModels;

namespace IMS.Application.ServiceInterface
{
    public interface IIMSMenu:IBaseInterface<object>
    {
        Task<dynamic> GetAllMenu();
        Task<dynamic> CreateOrUpdateMenu(IMSMenuVm data);
        Task<dynamic> GetMenuById(int id);
        Task<dynamic> DeleteMenu(int id);
    }
}