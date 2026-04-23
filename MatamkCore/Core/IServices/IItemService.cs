using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IServices
{
    public interface IItemService
    {
        public Item AddItem(ItemD item);
        public Item UpdateItem(ItemD item, int id);
        public ItemD GetItem(int id);
        public List<ItemD> GetAllItems();
         public List<ItemD> GetItemsByCategory(int categoryId);
          public List<ItemD> GetItemsByCountry(int countryId);
        public List<ItemD> GetItensByCountryAndCategory(int? countryId, int? categoryId);
    }
}
