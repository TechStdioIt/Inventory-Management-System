using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface ICategory :IBaseInterface<object>
    {
        Task<dynamic> GetAllCategory();
        Task<dynamic> CreateOrUpdate(CategoryVM data);
        Task<dynamic> GetCategoryById(int id);
    }
}
