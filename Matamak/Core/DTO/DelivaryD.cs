using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class DelivaryD
    {
        public int orderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemsD> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string CustomerName { get; set; }
        
    }
}
