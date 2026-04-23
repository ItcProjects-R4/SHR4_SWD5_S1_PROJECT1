using Core.DTO;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class OredrItemsServices : IOrderItemsService
    {
        private readonly DataContext dataContext;

        public OredrItemsServices(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public OrderItemsD GetOrderItem(OrderItems orderItem)
        {
            var orderItemDto = new OrderItemsD();
           
            if (orderItem != null)
            {
                orderItem.Name = orderItem.Name;
                orderItem.PriceForOne = orderItem.PriceForOne;
                orderItem.Quantity = orderItem.Quantity;
                orderItem.Note = orderItem.Note;
                orderItem.TotalPrice = orderItem.TotalPrice;
            }
            return orderItemDto;
        }

        OrderItems IOrderItemsService.AddOrderItem(OrderItemsD orderItem)
        {
            var orderItemToAdd = new OrderItems
            {
                Name = orderItem.Name,
                PriceForOne = orderItem.PriceForOne,
                Quantity = orderItem.Quantity,
                Note = orderItem.Note,
                TotalPrice = orderItem.TotalPrice
            };
            return orderItemToAdd;
        }

        OrderItems IOrderItemsService.UpdateOrderItem(OrderItemsD orderItem, int orderItemId)
        {
            var orderItemToUpdate = dataContext.OrderItems.Find(orderItemId);
            if (orderItemToUpdate != null)
            {
                orderItemToUpdate.Name = orderItem.Name;
                orderItemToUpdate.PriceForOne = orderItem.PriceForOne;
                orderItemToUpdate.Quantity = orderItem.Quantity;
                orderItemToUpdate.Note = orderItem.Note;
                orderItemToUpdate.TotalPrice = orderItem.TotalPrice;
            }
            return orderItemToUpdate;
        }
    }
}
