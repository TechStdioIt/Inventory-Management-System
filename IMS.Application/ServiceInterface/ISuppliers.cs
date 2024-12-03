using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface ISuppliers : IBaseInterface<object>
    {
        Task<dynamic> GetAllSuppliers();
        Task<dynamic> CreateOrUpdate(SuppliersVm data);
        Task<dynamic> GetSuppliersById(int id);
        Task<dynamic> DeleteSuppliers(int id);
    }
}
