using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Domain.Models;

namespace IMS.Application.ServiceInterface
{
    public interface IIMSMenu:IBaseInterface<IMSMenu>
    {
        Task<IEnumerable<IMSMenu>> GetMenuAsync();
    }
}
