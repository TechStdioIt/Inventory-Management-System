using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IStoreType : IBaseInterface<object>
    {
        Task<dynamic> CreateOrUpdateStore(StoreTypeVM data);
        Task<dynamic> GetDataById(int id);
        Task<dynamic> DeleteDataById(int id);

    }
}
