using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Core.ModelView;
using Core.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Reprosatory
{
    public class TakeAwayOrderRepo : ITakeAwayOrderRepo
    {
        private readonly ITakeAwayOrderService takeAwayOrderService;
        private readonly IOrderItemsService orderItemsService;
        private readonly DataContext dataContext;

        public TakeAwayOrderRepo(
            ITakeAwayOrderService takeAwayOrderService,
            IOrderItemsService orderItemsService,
            DataContext dataContext)
        {
            this.takeAwayOrderService = takeAwayOrderService;
            this.orderItemsService = orderItemsService;
            this.dataContext = dataContext;
        }

        public void AddTakeAwayOrder(TakeAwayD order)
        {
            var newOrder = (TakeawayOrder)takeAwayOrderService.AddTakeAwayOrder(order);
            dataContext.TakeawayOrders.Add(newOrder);
            dataContext.SaveChanges();
        }

        public List<TakeAwayOrderMV> GetAllTakeAwayOrders()
        {
            return dataContext.TakeawayOrders
                .Include(o => o.Items)
                .Select(Map)
                .ToList();
        }

        public TakeAwayOrderMV GetTakeAwayOrder(int orderNumber)
        {
            var order = dataContext.TakeawayOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderNumber || o.orderNumber == orderNumber);

            if (order == null)
            {
                throw new Exception("Takeaway order not found.");
            }

            return Map(order);
        }

        public void RemoveTakeAwayOrder(int orderNumber)
        {
            var order = dataContext.TakeawayOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderNumber || o.orderNumber == orderNumber);

            if (order == null)
            {
                throw new Exception("Takeaway order not found.");
            }

            dataContext.OrderItems.RemoveRange(order.Items);
            dataContext.TakeawayOrders.Remove(order);
            dataContext.SaveChanges();
        }

        public void UpdateTakeAwayOrder(TakeAwayD order, int orderNumber)
        {
            var existingOrder = dataContext.TakeawayOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderNumber || o.orderNumber == orderNumber);

            if (existingOrder == null)
            {
                throw new Exception("Takeaway order not found.");
            }

            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderDate = order.OrderDate ?? existingOrder.OrderDate;
            existingOrder.CustomerName = order.CustomerName;
            dataContext.OrderItems.RemoveRange(existingOrder.Items);
            existingOrder.Items = order.Items.Select(orderItemsService.AddOrderItem).ToList();

            dataContext.TakeawayOrders.Update(existingOrder);
            dataContext.SaveChanges();
        }

        private TakeAwayOrderMV Map(TakeawayOrder order)
        {
            return new TakeAwayOrderMV
            {
                Id = order.Id,
                OrderNumber = order.orderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CustomerName = order.CustomerName,
                IsPaid = order.IsPaid,
                Items = order.Items.Select(orderItemsService.GetOrderItem).ToList()
            };
        }
    }
}
