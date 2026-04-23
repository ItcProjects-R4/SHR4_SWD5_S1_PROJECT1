using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int orderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItems> Items { get; set; }
        public decimal TotalPrice { get; set; } 
        


    }
}
