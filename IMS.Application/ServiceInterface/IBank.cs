using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IBank:IBaseInterface<object>
    {
        Task<dynamic> GetAllBank();
        Task<dynamic> CreateOrUpdateBank(BankVM data);
        Task<dynamic> GetBankById(int id);
        Task<dynamic> DeleteBank(int id);
    }
}
