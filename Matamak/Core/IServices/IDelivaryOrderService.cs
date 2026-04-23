using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IServices
{
    public interface IDelivaryOrderService
    {
            public DeliveryOrder AddDelivaryOrder(DelivaryD order);
            public DeliveryOrder UpdateDelivaryOrder(DelivaryD order, int orderNumber);
            public DelivaryD GetDelivaryOrder(int orderNumber);
            public List<DelivaryD> GetAllDelivaryOrders();
    }
}
