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
        Task<List<OrderTypeViewModel>> GetAllOrderTypes();
        Task<decimal?> GetSelectedOrderTypeItemPriceOrderType(int orderTypeId);
        Task<bool> Add(OrderTypeViewModel ordersTypeViewModel);
        Task<bool> Update(OrderTypeViewModel ordersTypeViewModel);
    }
}
