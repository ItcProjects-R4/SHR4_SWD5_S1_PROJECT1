using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class TakeAwayD
    {
        public int orderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemsD> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
