using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class DineinOrder: Order
    {
        public decimal TableNumber { get; set; }
        public decimal ServiceCharge { get; set; } = 20;

           
    }
}
