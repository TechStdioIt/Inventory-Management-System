using IMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface IProduct
    {
        Task<dynamic> GetAllProductData();
        Task<dynamic> CreateOrUpdate(ProductsVM data);
        Task<dynamic> GetProductById(int id);
        Task<dynamic> DeleteProductData (int id);
    }
}
