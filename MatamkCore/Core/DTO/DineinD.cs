using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class DineinD
    {
        public int orderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemsD> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TableNumber { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
