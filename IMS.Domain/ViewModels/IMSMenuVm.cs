using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class IMSMenuVm
    {
        public int id { get; set; }
        public int? parentId { get; set; } 
        public string? title { get; set; }
        public string? type { get; set; }
        public string? url { get; set; }
        public string? icon { get; set; }
        public bool? target { get; set; }
        public bool? breadcrumbs { get; set; } 
        public string? classes { get; set; }
    }
}
