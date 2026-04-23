using Core.DTO;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class DineinOredrServices : IDieninOrderService
    {
        private readonly DataContext dataContext;
        private readonly IOrderItemsService orderItemsService;

        public DineinOredrServices(DataContext dataContext, IOrderItemsService orderItemsService)
        {
            this.dataContext = dataContext;
            this.orderItemsService = orderItemsService;
        }
        DineinOrder IDieninOrderService.AddDineinOrder(DineinD order)
        {
            var dineinOrder = new DineinOrder
            {
                orderNumber = order.orderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                TableNumber = order.TableNumber,
                ServiceCharge = order.ServiceCharge
            };
            return dineinOrder;
        }

        List<DineinD> IDieninOrderService.GetAllDineinOrders()
        {
            var dineinOrders = new List<DineinD>();
            var dineinOrderList = dataContext.DineinOrders.ToList();
            foreach (var order in dineinOrderList)
            {
                var dineinD = new DineinD
                {
                    orderNumber = order.orderNumber,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    TableNumber = order.TableNumber,
                    ServiceCharge = order.ServiceCharge
                };
                dineinOrders.Add(dineinD);
            }
            return dineinOrders;
        }
        DineinD IDieninOrderService.GetDineinOrder(int orderNumber)
        {
            var order = dataContext.DineinOrders.FirstOrDefault(o => o.orderNumber == orderNumber);
            var dineinD = new DineinD
            {
                orderNumber = order.orderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                TableNumber = order.TableNumber,
                ServiceCharge = order.ServiceCharge
            };
            foreach (var item in order.Items)
            {
                var orderItemsD = orderItemsService.GetOrderItem(item);
                dineinD.Items.Add(orderItemsD);
            } return dineinD;
        }

        DineinOrder IDieninOrderService.UpdateDineinOrder(DineinD order, int orderNumber)
        {
            var dineinOrder = dataContext.DineinOrders.FirstOrDefault(o => o.orderNumber == orderNumber);
            dineinOrder.OrderDate = order.OrderDate;

            dineinOrder.TotalPrice = order.TotalPrice;
            dineinOrder.TableNumber = order.TableNumber;
            dineinOrder.ServiceCharge = order.ServiceCharge;
            return dineinOrder;
        }
    }
}
