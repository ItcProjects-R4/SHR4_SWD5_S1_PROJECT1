using Core.DTO;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class IteamServices : IItemService
    {
        private readonly DataContext dataContext;

        public IteamServices(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Item AddItem(ItemD item)
        {
            Item item1 = new Item();
            item1.Name = item.Name;
            item1.Description = item.Description;
            item1.Price = item.Price;
            item1.ImageUrl = item.ImageUrl;
            item1.CatogryId = item.CatogryId;
            item1.CountryId = item.CountryId;
            item1.IsAvailable = item.IsAvailable;

            return item1;

        }

        public List<ItemD> GetItensByCountryAndCategory(int? countryId, int? categoryId)
        {
            var items = new List<ItemD>();
            var item1 = dataContext.Items.Where(i => i.CountryId == countryId && i.CatogryId == categoryId).FirstOrDefault();
            foreach (var item in items)
            {
                items.Add(new ItemD
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    CatogryId = item.CatogryId,
                    CountryId = item.CountryId,
                    IsAvailable = item.IsAvailable
                });
            }
            return items;
        }

        public Item UpdateItem(ItemD item, int id)
        {
            var item1 = dataContext.Items.Find(id);
            if (item1 != null)
            {
                item1.Name = item.Name;
                item1.Description = item.Description;
                item1.Price = item.Price;
                item1.ImageUrl = item.ImageUrl;
                item1.CatogryId = item.CatogryId;
                item1.CountryId = item.CountryId;
                item1.IsAvailable = item.IsAvailable;
                return item1;
            }
            else
            {
                return null;
            }
        }

        List<ItemD> IItemService.GetAllItems()
        {
            var items = new List<ItemD>();
            var item1 = dataContext.Items.ToList();
            foreach (var item in item1)
            {
                items.Add(new ItemD
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    CatogryId = item.CatogryId,
                    CountryId = item.CountryId,
                    IsAvailable = item.IsAvailable
                });
            }
            return items;
        }

        ItemD IItemService.GetItem(int id)
        {
            var item = dataContext.Items.Find(id);
            if (item != null)
            {
                return new ItemD
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    CatogryId = item.CatogryId,
                    CountryId = item.CountryId,
                    IsAvailable = item.IsAvailable
                };
            }
            return null;
        }

        List<ItemD> IItemService.GetItemsByCategory(int categoryId)
        {
            var items = new List<ItemD>();
            var item = dataContext.Items.Where(i => i.CatogryId == categoryId).ToList();
            foreach (var i in item)
            {
                items.Add(new ItemD
                {
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    CatogryId = i.CatogryId,
                    CountryId = i.CountryId,
                    IsAvailable = i.IsAvailable
                });
            }
            return items;
        }

        List<ItemD> IItemService.GetItemsByCountry(int countryId)
        {
            var items = new List<ItemD>();
            var item = dataContext.Items.Where(i => i.CountryId == countryId).ToList();
            foreach (var i in item)
            {
                items.Add(new ItemD
                {
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    CatogryId = i.CatogryId,
                    CountryId = i.CountryId,
                    IsAvailable = i.IsAvailable
                });
            }
            return items;
        }
    }
}
