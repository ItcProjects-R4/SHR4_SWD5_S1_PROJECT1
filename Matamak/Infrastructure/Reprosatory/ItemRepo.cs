using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reprosatory
{
    public class ItemRepo : IItemRepo
    {
        private readonly DataContext dataContext;
        private readonly IItemService _item;

        public ItemRepo(DataContext dataContext, IItemService item)
        {
            this.dataContext = dataContext;
            this._item = item;
        }
        void IItemRepo.AddItem(ItemD item)
        {
          var item1=  _item.AddItem(item);

            dataContext.Items.Add(item1);
            dataContext.SaveChanges();
        }

        List<ItemD> IItemRepo.GetAllItems()
        {
            var items1 = _item.GetAllItems();
            return items1;
        }

        ItemD IItemRepo.GetItemById(int id)
        {
              var item1 = _item.GetItem(id);
            return item1;
        }

        List<ItemD> IItemRepo.GetItemsByCategory(int categoryId)
        {
            var items1 = _item.GetItemsByCategory(categoryId);
            return items1;
        }   
        List<ItemD> IItemRepo.GetItemsByCountry(int countryId)
        {
            var items1 = _item.GetItemsByCountry(countryId);
            return items1;
        }
           
        List<ItemD> IItemRepo.GetItensByCountryAndCategory(int? countryId, int? categoryId)
        {
            if (!countryId.HasValue)
            {
                var items1 = _item.GetItemsByCategory(categoryId.Value);

                return items1;
            }
            else if (!categoryId.HasValue)
            {
                var items1 = _item.GetItemsByCountry(countryId.Value);
                return items1;
            }
            else
            {
                var items1 = _item.GetItensByCountryAndCategory(countryId, categoryId);
                return items1;
            }

        }

        void IItemRepo.RemoveItem(int id)
        {
            var item = dataContext.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                dataContext.Items.Remove(item);
                dataContext.SaveChanges();
            }

        }

        void IItemRepo.UpdateItem(ItemD item, int id)
        {
            var item1 =_item.UpdateItem(item, id);


            dataContext.Items.Update(item1);
            dataContext.SaveChanges();

        }
    }
}
