using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Reprosatory
{
    public class DelivaryOrderRepo : IDeliveryOrderRepo
    {
        private readonly IDelivaryOrderService delivaryOrderService;
        private readonly DataContext dataContext;

        public DelivaryOrderRepo(IDelivaryOrderService delivaryOrderService , DataContext dataContext)
        {
            this.delivaryOrderService = delivaryOrderService;
            this.dataContext = dataContext;
        }
        void IDeliveryOrderRepo.AddDeliveryOrder(DelivaryD order)
        {

            var result = delivaryOrderService.AddDelivaryOrder(order);
          
            dataContext.DeliveryOrders.Add(result);
            dataContext.SaveChanges();

        }

        List<DelivaryD> IDeliveryOrderRepo.GetAllDeliveryOrders()
        {
            return delivaryOrderService.GetAllDelivaryOrders();
        }

        DelivaryD IDeliveryOrderRepo.GetDeliveryOrder(int deliveryId)
        {
            var order = delivaryOrderService.GetDelivaryOrder(deliveryId);
           return order;

        }

        void IDeliveryOrderRepo.RemoveDeliveryOrder(int deliveryId)
        {
           dataContext.DeliveryOrders.Remove(dataContext.DeliveryOrders.Find(deliveryId));
            dataContext.SaveChanges();
        }

        void IDeliveryOrderRepo.UpdateDeliveryOrder(DelivaryD order, int deliveryId)
        {
            var existingOrder = delivaryOrderService.UpdateDelivaryOrder(order, deliveryId);
            dataContext.DeliveryOrders.Update(existingOrder);
            dataContext.SaveChanges();

        }
    }
}
