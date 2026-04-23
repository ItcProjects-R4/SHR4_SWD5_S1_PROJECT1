using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class DeliveryOrder: Order
    {
        public bool IsPaid { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal DeliveryFee { get; set; } = 30;

    }
}
