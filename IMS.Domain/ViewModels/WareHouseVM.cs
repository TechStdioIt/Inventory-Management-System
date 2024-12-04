using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class WareHouseVM           
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string wareHouseName { get; set; }
        public DateTime openTime { get; set; }
        public DateTime closeTime { get; set; }
        public string note { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string updatedBy { get; set; }
        public bool isDeleted { get; set; }
        public bool isActive { get; set; }

    }
}
