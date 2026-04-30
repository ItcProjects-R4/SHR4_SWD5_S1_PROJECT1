using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class DeliveryOrder: Order
    {
        public enum DeliveryStatus
        {
            Pending,
            OutForDelivery,
            Delivered,
            Canceled
        }
        
        public bool IsPaid { get; set; }
        public string CustomerUsername { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal DeliveryFee { get; set; } = 30;

    }
}
