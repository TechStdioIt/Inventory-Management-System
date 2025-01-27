using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IPurchaseType :IBaseInterface<object>
    {
        Task<dynamic> GetAllPurchaseType();
        Task<dynamic> CreateOrUpdatePurchaseType(PurchaseTypeVM data);
        Task<dynamic> GetPurchaseTypeById(int id);
        Task<dynamic> DeletePurchaseType(int id);
    }
}
