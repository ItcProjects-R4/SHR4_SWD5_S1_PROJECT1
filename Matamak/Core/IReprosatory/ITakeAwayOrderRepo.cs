using Core.DTO;
using Core.ModelView;
using System.Collections.Generic;

namespace Core.IReprosatory
{
    public interface ITakeAwayOrderRepo
    {
        void AddTakeAwayOrder(TakeAwayD order);
        void RemoveTakeAwayOrder(int orderNumber);
        void UpdateTakeAwayOrder(TakeAwayD order, int orderNumber);
        TakeAwayOrderMV GetTakeAwayOrder(int orderNumber);
        List<TakeAwayOrderMV> GetAllTakeAwayOrders();
    }
}
