using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class SuppliersVm
    {
        public int id { get; set; }
        public string? userId { get; set; }
        public string? companyName { get; set; }
        public string? contactName { get; set; }
        public string? contactTitle { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? province { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
    }
}
