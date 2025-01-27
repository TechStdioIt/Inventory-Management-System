using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
    public class PurchasOrderVM
    {
        public int id { get; set; }
        public DateTime? orderDate { get; set; }
        public int? supplierId { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? shippingCost { get; set; }
        public int? paymentMethodId { get; set; }
        public string? userId { get; set; }
        public bool? isActive { get; set; }

        public List<PurchasDetailVM>? purchasList { get; set; } // List of product details
        public PurchasOrderVM()
        {
            purchasList = new List<PurchasDetailVM>();
        }
    }

    public class PurchasDetailVM
    {
        public int id { get; set; }
        public int? purchaseOrderId { get; set; }
        public int? productId { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? totalPrice { get; set; }
    }
}
