using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface ICustomer:IBaseInterface<object>
    {
        Task<dynamic> GetAllCustomer();
        Task<dynamic> CreateOrUpdateCustomer(CustomerVM data);
        Task<dynamic> GetCustomerById(int id);
        Task<dynamic> DeleteCustomer(int id);
    }
}
