using Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IReprosatory
{
    public interface ITakeAwayOrderRepo
    {
        public void AddTakeAwayOrder(TakeAwayD order);
        public void RemoveTakeAwayOrder(int orderNumber);
        public void UpdateTakeAwayOrder(TakeAwayD order, int orderNumber);
        public TakeAwayD GetTakeAwayOrder(int orderNumber);
        public List<TakeAwayD> GetAllTakeAwayOrders();
    }
}
