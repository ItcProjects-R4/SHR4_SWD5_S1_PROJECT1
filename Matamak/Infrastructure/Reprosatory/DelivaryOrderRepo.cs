using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Core.ModelView;
using Infrastructure.Context;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Markup;

namespace Infrastructure.Reprosatory
{
    public class DelivaryOrderRepo : IDeliveryOrderRepo
    {
        private readonly IDelivaryOrderService delivaryOrderService;
        private readonly IHubContext<OrderHub> hubContext;
        private readonly IOrderItemsService orderItemsService;
        private readonly DataContext dataContext;

        public DelivaryOrderRepo(IDelivaryOrderService delivaryOrderService,IHubContext<OrderHub> hubContext ,IOrderItemsService orderItemsService, DataContext dataContext)
        {
            this.delivaryOrderService = delivaryOrderService;
            this.hubContext = hubContext;
            this.orderItemsService = orderItemsService;
            this.dataContext = dataContext;
        }

        public void CancelDeliveryOrder(int deliveryId)
        {
            var order = dataContext.DeliveryOrders.Find(deliveryId);
            if (order == null)
            {
                throw new Exception($"Delivery order from service is null for ID {deliveryId}.");
            }
            if (order.Status != "Delivered" && order.Status != "OutForDelivery")
            {
                order.Status = "Canceled";
                dataContext.DeliveryOrders.Update(order);
                dataContext.SaveChanges();
               
            }
            else
            {
                throw new Exception($"Cannot cancel an order that is already delivered or out for delivery for ID {deliveryId}.");
            }
               
          
        }

        public List<DeliveryOrderMV> GetDeliveryOrderByCustomerId(string custmorusername)
        {
            var order = dataContext.DeliveryOrders.Where(o=> o.CustomerName == custmorusername);

            if (order == null || !order.Any())
            {
                throw new Exception($"No delivery orders found for customer username {custmorusername}.");
            }
            List<DeliveryOrderMV> deliveryOrdersMV = new List<DeliveryOrderMV>();

            foreach (var o in order)
            {
                var orderMV = new DeliveryOrderMV
                {
                    Id = o.Id,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                    OrderNumber = o.orderNumber,
                  
                };
                deliveryOrdersMV.Add(orderMV);
            }

            return deliveryOrdersMV;
        }

        async Task IDeliveryOrderRepo.AddDeliveryOrder(DelivaryD order)
        {

            var result = delivaryOrderService.AddDelivaryOrder(order);
          
            dataContext.DeliveryOrders.Add(result);
            dataContext.SaveChanges();
            await hubContext.Clients.Group("Cashiers").SendAsync("ReceiveNewOrder", order);


        }

        List<DeliveryOrderMV> IDeliveryOrderRepo.GetAllDeliveryOrders()
        {
           var orders = delivaryOrderService.GetAllDelivaryOrders();
            if (orders == null || orders.Count == 0)
            {
                 throw new Exception("Delivery orders from service are null or empty.");
            }
            return orders;
        }

        DeliveryOrderMV IDeliveryOrderRepo.GetDeliveryOrder(int deliveryId)
        {
            var order = delivaryOrderService.GetDelivaryOrder(deliveryId);
            if (order == null)
            {
                throw new Exception($"Delivery order from service is null for ID {deliveryId}.");
            }
            return order;

        }

        void IDeliveryOrderRepo.HandOrderToCustmor(int deliveryId)
        {
            var order = dataContext.DeliveryOrders.Find(deliveryId);
            if (order != null && order.Status != "Pending" && order.Status != "Canceled")
            {
                order.Status = "Delivered";
                dataContext.DeliveryOrders.Update(order);
                dataContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Cannot hand order to customer. Order not found or already delivered/canceled for ID {deliveryId}.");
            }
        }

        async Task IDeliveryOrderRepo.HandOrderToDriver(int deliveryId)
        {
            var order = dataContext.DeliveryOrders.Find(deliveryId);
            if (order != null && order.Status != "Delivered" && order.Status != "Canceled")
            {
                order.Status = "OutForDelivery";
                dataContext.DeliveryOrders.Update(order);
                dataContext.SaveChanges();
                await hubContext.Clients.Group("User" +order.CustomerUsername).SendAsync("ReceiveOrderStatusUpdate", $"Your order with Number {order.orderNumber} is now out for delivery.");
            }
            else
            {
                throw new Exception($"Cannot hand order to driver. Order not found or already delivered/canceled for ID {deliveryId}.");
            }
        }

        void IDeliveryOrderRepo.RemoveDeliveryOrder(int deliveryId)
        {
           var order = dataContext.DeliveryOrders.Find(deliveryId);
           if (order != null)
           {
                foreach (var item in dataContext.OrderItems)
                {
                    orderItemsService.DeleteOrderItem(item);
                }
                dataContext.DeliveryOrders.Remove(order);
               dataContext.SaveChanges();
           }
           else
                throw new Exception($"Delivery order from service is null for ID {deliveryId}.");
        }

        void IDeliveryOrderRepo.UpdateDeliveryOrder(DelivaryD order, int deliveryId)
        {

            var existingOrder = delivaryOrderService.UpdateDelivaryOrder(order, deliveryId);
            if (existingOrder != null)
            {

                dataContext.DeliveryOrders.Update(existingOrder);
                dataContext.SaveChanges();
            } else throw new Exception($"Delivery order from service is null for ID {deliveryId}.");

        }
    }
}
