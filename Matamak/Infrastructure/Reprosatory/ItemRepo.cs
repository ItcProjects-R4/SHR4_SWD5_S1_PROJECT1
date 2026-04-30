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
            if (item1 == null)
            {
                throw new Exception("Failed to add item.");
            }

            dataContext.Items.Add(item1);
            dataContext.SaveChanges();
        }

        List<ItemsModelView> IItemRepo.GetAllItems()
        {
            var items1 = _item.GetAllItems();
            if (items1 == null)
            {
                throw new Exception("Item From Service is null.");
            }
            return items1;
        }

        ItemsModelView IItemRepo.GetItemById(int id)
        {
              var item1 = _item.GetItem(id);
            if (item1 == null)
            {
                throw new Exception($"Item From Service is null for ID {id}.");
            }
            return item1;
        }

        List<ItemsModelView> IItemRepo.GetItemsByCategory(int categoryId)
        {
            var items1 = _item.GetItemsByCategory(categoryId);
            if (items1 == null || items1.Count == 0)
            {
               throw new Exception($"Items From Service are null for Category ID {categoryId}.");
            }
            return items1;
        }   
        List<ItemsModelView> IItemRepo.GetItemsByCountry(int countryId)
        {
            var items1 = _item.GetItemsByCountry(countryId);
            if (items1 == null || items1.Count == 0)
            {
                throw new Exception($"Items From Service are null for Country ID {countryId}.");
            }
            return items1;
        }
           
        List<ItemsModelView> IItemRepo.GetItensByCountryAndCategory(int? countryId, int? categoryId)
        {
            if (!countryId.HasValue)
            {
                var items1 = _item.GetItemsByCategory(categoryId.Value);
                if (items1 == null || items1.Count == 0)
                {
                    throw new Exception($"Items From Service are null .");
                }

                return items1;
            }
            else if (!categoryId.HasValue)
            {
                var items1 = _item.GetItemsByCountry(countryId.Value);
                if (items1 == null || items1.Count == 0)
                {
                   throw new Exception($"Items From Service are null for Country ID {countryId}.");
                }
                return items1;
            }
            else
            {
                var items1 = _item.GetItensByCountryAndCategory(countryId, categoryId);
                if (items1 == null || items1.Count == 0)
                {
                    throw new Exception($"Items From Service are null for Country ID {countryId} and Category ID {categoryId}.");
                }
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
                else
                {
                    throw new Exception($"Item with ID {id} not found.");
            }

        }

        void IItemRepo.UpdateItem(ItemD item, int id)
        {
            var item1 =_item.UpdateItem(item, id);
            if (item1 == null)
            { 
                throw new Exception($"Item From Service is null for ID {id}.");
            }

            dataContext.Items.Update(item1);
            dataContext.SaveChanges();

        }
    }
}
