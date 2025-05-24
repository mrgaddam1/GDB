using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public  interface IOrderTypesRepository
    {
        Task<List<OrdersTypeViewModel>> GetAllOrderTypes();
        Task<decimal?> GetSelectedOrderTypeItemPriceOrderType(int orderTypeId);
        Task<bool> Add(OrdersTypeViewModel ordersTypeViewModel);
        Task<bool> Update(OrdersTypeViewModel ordersTypeViewModel);
    }
}
