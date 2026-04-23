using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IServices
{
    public interface IOrderItemsService
    {
        public OrderItems AddOrderItem(OrderItemsD orderItem);
        public OrderItems UpdateOrderItem(OrderItemsD orderItem, int orderItemId);
        public OrderItemsD GetOrderItem(OrderItems orderItem);

    }
}
