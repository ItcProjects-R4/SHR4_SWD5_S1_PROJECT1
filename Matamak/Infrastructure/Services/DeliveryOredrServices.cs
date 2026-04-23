using Core.DTO;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class DeliveryOredrServices : IDelivaryOrderService
    {
        private readonly DataContext dataContext;
        private readonly IOrderItemsService orderItemsService;

        public DeliveryOredrServices(DataContext dataContext, IOrderItemsService orderItemsService)
        {
            this.dataContext = dataContext;
            this.orderItemsService = orderItemsService;
        }
        DeliveryOrder IDelivaryOrderService.AddDelivaryOrder(DelivaryD order)
        {
            var newOrder = new DeliveryOrder {Items = new List<OrderItems>()};

            newOrder.OrderDate = order.OrderDate;
            newOrder.TotalPrice = order.TotalPrice;
            newOrder.DeliveryAddress = order.DeliveryAddress;
            newOrder.ContactNumber = order.ContactNumber;
            newOrder.CustomerName = order.CustomerName;
            decimal? sum = 0;
            foreach (var item in order.Items)
            {
               newOrder.Items.Add(orderItemsService.AddOrderItem(item));
                sum+= item.TotalPrice;
            }
            ;
            return newOrder;
        }

        List<DelivaryD> IDelivaryOrderService.GetAllDelivaryOrders()
        {
            var orders = dataContext.DeliveryOrders.ToList();
            var orderDtos = new List<DelivaryD>();
            foreach (var order in orders)
            {
                var orderDto = new DelivaryD
                {
                    orderNumber = order.orderNumber,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    DeliveryAddress = order.DeliveryAddress,
                    ContactNumber = order.ContactNumber,
                    CustomerName = order.CustomerName
                };
                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }

        DelivaryD IDelivaryOrderService.GetDelivaryOrder(int orderNumber)
        {
            var order = dataContext.DeliveryOrders.FirstOrDefault(o => o.orderNumber == orderNumber);
            var orderDto = new DelivaryD
            {
                orderNumber = order.orderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                DeliveryAddress = order.DeliveryAddress,
                ContactNumber = order.ContactNumber,
                CustomerName = order.CustomerName
            };
            foreach (var item in order.Items)
            {
                orderDto.Items.Add(orderItemsService.GetOrderItem(item));
            }


                return orderDto;
        }

        DeliveryOrder IDelivaryOrderService.UpdateDelivaryOrder(DelivaryD order, int orderNumber)
        {
            var DeliveryOrder = dataContext.DeliveryOrders.FirstOrDefault(o => o.orderNumber == orderNumber);
            if (DeliveryOrder != null)
            {
                DeliveryOrder.TotalPrice = order.TotalPrice;
                DeliveryOrder.DeliveryAddress = order.DeliveryAddress;
                DeliveryOrder.ContactNumber = order.ContactNumber;
                DeliveryOrder.CustomerName = order.CustomerName;
                
            }
            return DeliveryOrder;
        }
    }
}
