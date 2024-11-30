using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models
{
    public class IMSMenu
    {
        public int Id { get; set; } // Auto-incrementing Id (Primary Key)
        public int? ParentId { get; set; } // Nullable parentId
        public string? Title { get; set; } // Nullable nvarchar(255)
        public string? Type { get; set; } // Nullable nvarchar(50)
        public string? Url { get; set; } // Nullable nvarchar(500)
        public string? Icon { get; set; } // Nullable nvarchar(255)
        public bool? Target { get; set; } // Nullable bit (boolean)
        public bool? Breadcrumbs { get; set; } // Nullable bit (boolean)
        public string? Classes { get; set; } // Nullable nvarchar(255)
        [NotMapped]
        public List<IMSMenu >Children { get; set; }
    }
}
