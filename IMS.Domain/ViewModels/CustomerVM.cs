﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class CustomerVM
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public int? customerTypeId { get; set; }
        public string? city { get; set; }
        public int? cp { get; set; }
        public string userId {  get; set; }
        public string? zipCode { get; set; }
        public DateTime? createdAt { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string? updatedBy { get; set; }
        public bool? isDeleted { get; set; }
        public bool? isActive { get; set; } 
    }
}
