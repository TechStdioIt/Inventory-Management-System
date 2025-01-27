using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class ProductsVM
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? categoryId { get; set; }
        public decimal? price { get; set; }
        public int? quantityInStock { get; set; }
        public int? userId { get; set; }
        public int? isActive { get; set; }
    }
}
