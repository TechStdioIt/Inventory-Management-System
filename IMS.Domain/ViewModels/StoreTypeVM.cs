using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class StoreTypeVM
    {
        public int id { get; set; }
        public string? name { get; set; }
        public DateTime createdAt { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string? updatedBy { get; set; }
        public bool isDelete { get; set; }
        public bool isActive { get; set; } 
    }
}
