using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class PurchaseTypeVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public bool isActive { get; set; }

    }
}
