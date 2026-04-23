using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IReprosatory
{
    public interface IItemRepo
    {
        public void AddItem(ItemD item);
        public void RemoveItem(int id);
        public void UpdateItem( ItemD item, int id);
        public ItemD GetItemById(int id);
        public List<ItemD> GetAllItems();
        public List<ItemD> GetItemsByCategory(int categoryId);
         public List<ItemD> GetItemsByCountry(int countryId);
        public List<ItemD> GetItensByCountryAndCategory(int? countryId, int? categoryId);
    }
}
