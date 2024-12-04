using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IWareHouse : IBaseInterface<object>
    {
        Task<dynamic> GetAllWareHouse();
        Task<dynamic> CreateOrUpdateWareHouse(WareHouseVM data);
        Task<dynamic> GetWareHouseById(int id);
        Task<dynamic> DeleteWareHouse(int id);
    }
}
