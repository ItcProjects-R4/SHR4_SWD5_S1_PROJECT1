using Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IReprosatory
{
    public interface IDeliveryOrderRepo
    {
        public void AddDeliveryOrder(DelivaryD order);
        public void RemoveDeliveryOrder(int deliveryId);
        public void UpdateDeliveryOrder(DelivaryD order, int deliveryId);
        public DelivaryD GetDeliveryOrder(int deliveryId);
        public List<DelivaryD> GetAllDeliveryOrders();

    }
}
