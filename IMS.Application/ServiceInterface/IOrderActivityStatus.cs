using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IOrderActivityStatus : IBaseInterface<object>
    {
        Task<dynamic> GetAllOrderActivityStatus();
        Task<dynamic> CreateOrUpdateOrderActivityStatus(OrderActivityStatusVM data);
        Task<dynamic> GetOrderActivityStatusById(int id);
        Task<dynamic> DeleteOrderActivityStatus(int id);
    }
}
