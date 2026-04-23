using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
