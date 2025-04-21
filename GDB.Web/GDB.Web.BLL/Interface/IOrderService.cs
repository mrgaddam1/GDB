using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IOrderService
    {
        Task<T?> GetAllOrders<T>();
        Task<bool?> Add(OrdersViewModel ordersViewModel);
        Task<bool?> Update(OrdersViewModel ordersViewModel);
        Task<T?> GetMaxWeekId<T>();

        Task<T?> GetAllOrdersWeekly<T>();
        Task<T?> GetAllOrdersByWeekly<T>();
        Task<T?> GetAllOrdersByMonthly<T>();
        Task<T?> GetAllOrdersByQuarterly<T>();
        Task<T?> GetAllOrdersByHalfYearly<T>();
        Task<T?> GetAllOrdersByYearly<T>();
        Task<T?> GetTotalOrdersByWeekwise<T>();
    }
}
