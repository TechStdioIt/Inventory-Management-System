using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IUnit :IBaseInterface<object>
    {
        Task<dynamic> GetAllUnit();
        Task<dynamic> CreateOrUpdateUnit(UnitVM data);
        Task<dynamic> GetUnitById(int id);
        Task<dynamic> DeletUnit(int id);
    }
}

