using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IOrdersRepository
    {
        Task<List<OrdersViewModel>> GetAllOrders();
        Task<bool>Add(OrdersViewModel order);
        Task<bool> Update(OrdersViewModel ordersViewModel);
        Task<int?> GetMaxWeekId();
        Task<List<OrdersReportViewModel>> GetAllOrdersWeekly();
        Task<List<OrdersReportViewModel>> GetAllOrdersByWeekly();
        Task<List<OrdersReportViewModel>> GetAllOrdersByMonthly();
        Task<List<OrdersReportViewModel>> GetAllOrdersByQuarterly();
        Task<List<OrdersReportViewModel>> GetAllOrdersByHalfYearly();
        Task<List<OrdersReportViewModel>> GetAllOrdersByYearly();
        Task<List<TotalOrdersByWeekViewModel>> GetTotalOrdersByWeekwise();
       
    }
}
