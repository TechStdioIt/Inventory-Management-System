using System;

namespace IMS.Domain.ViewModels
{
    public class OrderActivityStatusVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isDeleted { get; set; }
        public bool isActive { get; set; }
        public string createdBy { get; set; }
        public DateTime createdAt { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}