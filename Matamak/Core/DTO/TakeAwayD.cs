using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class TakeAwayD
    {
        [Required]
        public int orderNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public List<OrderItemsD> Items { get; set; }= new List<OrderItemsD>();
        public decimal TotalPrice { get; set; }
    }
}
