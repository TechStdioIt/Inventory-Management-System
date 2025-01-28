using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IPurchasOrder
    {

        Task<dynamic> CreateOrUpdatePurchaseOrder (string data);
        Task<dynamic> GetAllpurchaseOrder();
    }
}
