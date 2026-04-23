using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IServices
{
    public interface IDieninOrderService
    {
        public DineinOrder AddDineinOrder(DineinD order);
        public DineinOrder UpdateDineinOrder(DineinD order, int orderNumber);
        public DineinD GetDineinOrder(int orderNumber);
        public List<DineinD> GetAllDineinOrders();
    }
}
