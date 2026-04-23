using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class TakeAwayOredrServices : ITakeAwayOrderService
    {
        private readonly IOrderItemsService orderItemsService;
        private readonly DataContext dataContext;

        public TakeAwayOredrServices(IOrderItemsService orderItemsService,DataContext dataContext)
        {
            this.orderItemsService = orderItemsService;
            this.dataContext = dataContext;
        }
        Order ITakeAwayOrderService.AddTakeAwayOrder(TakeAwayD order)
        {
            var TakeAwayOrder = new Order();
            TakeAwayOrder.orderNumber = order.orderNumber;
            TakeAwayOrder.OrderDate = order.OrderDate;
            TakeAwayOrder.TotalPrice = order.TotalPrice;
            foreach (var item in order.Items) 
            {
                TakeAwayOrder.Items.Add(orderItemsService.AddOrderItem(item))  ;
            }
            return TakeAwayOrder ;
        }

        List<TakeAwayD> ITakeAwayOrderService.GetAllTakeAwayOrders()
        {
            var list = new List<TakeAwayD>();
            var orders= dataContext.Orders.ToList();
            foreach (var order in orders)
            {
               var takeawayorder = new TakeAwayD();
                takeawayorder.orderNumber = order.orderNumber;
                takeawayorder.OrderDate = order.OrderDate;
                takeawayorder.TotalPrice = order.TotalPrice;

                list.Add(takeawayorder);
            }
            return list ;
        }

        TakeAwayD ITakeAwayOrderService.GetTakeAwayOrder(int orderNumber)
        {
            var order = dataContext.Orders.FirstOrDefault(o => o.orderNumber == orderNumber);
            var takeawayorder = new TakeAwayD();
            takeawayorder.orderNumber = orderNumber;
                        takeawayorder.OrderDate = order.OrderDate;
                                    takeawayorder.TotalPrice = order.TotalPrice;
            foreach (var item in order.Items)
            {
                var orderItem = new OrderItemsD();
                orderItem.Name = item.Name;
                orderItem.Quantity = item.Quantity;
                orderItem.TotalPrice = item.TotalPrice;
                orderItem.PriceForOne = item.PriceForOne;
                takeawayorder.Items.Add(orderItem);
            }
            return takeawayorder ;
        }

        Order ITakeAwayOrderService.UpdateTakeAwayOrder(TakeAwayD order, int orderNumber)
        {
           var existingOrder = dataContext.Orders.FirstOrDefault(o => o.orderNumber == orderNumber);
            if (existingOrder != null)
            {
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.TotalPrice = order.TotalPrice;
                
               
                return existingOrder;
            }
            return null;
        }
    }
}
