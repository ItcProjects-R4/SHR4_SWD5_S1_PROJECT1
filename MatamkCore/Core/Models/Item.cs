using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        [ForeignKey("Catogry")]
        public int CatogryId { get; set; }
        public Category Catogry { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
