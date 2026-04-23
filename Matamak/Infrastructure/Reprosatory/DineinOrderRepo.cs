using Core.DTO;
using Core.IReprosatory;
using Core.IServices;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Reprosatory
{
    public class DineinOrderRepo : IDineinOrderRepo
    {
        private readonly IDieninOrderService dineinOrderService;
        private readonly DataContext dataContext;

        public DineinOrderRepo(IDieninOrderService dineinOrderService ,DataContext dataContext)
        {
            this.dineinOrderService = dineinOrderService;
            this.dataContext = dataContext;
        }
        void IDineinOrderRepo.AddDineinOrder(DineinD order)
        {
           var result= dineinOrderService.AddDineinOrder(order);
            dataContext.DineinOrders.Add(result);
            dataContext.SaveChanges();
        }

        List<DineinD> IDineinOrderRepo.GetAllDineinOrders()
        {
           var result =dineinOrderService.GetAllDineinOrders();
            return result;
        }

        DineinD IDineinOrderRepo.GetDineinOrder(int orderNumber)
        {
          var result = dineinOrderService.GetDineinOrder(orderNumber);
            return result;
        }

        void IDineinOrderRepo.RemoveDineinOrder(int orderNumber)
        {
            var order = dataContext.DineinOrders.Find(orderNumber);
            if (order != null)
            {
                dataContext.DineinOrders.Remove(order);
                dataContext.SaveChanges();
            }
        }

        void IDineinOrderRepo.UpdateDineinOrder(DineinD order, int orderNumber)
        {
            var existingOrder = dineinOrderService.UpdateDineinOrder(order, orderNumber);
            dataContext.DineinOrders.Update(existingOrder);
            dataContext.SaveChanges();
        }
    }
}
