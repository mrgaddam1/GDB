using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IOrdersTypeService
    {
        Task<T?> GetAllOrderTypes<T>();
        Task<decimal?> GetOrderTypePriceByOrderType(int orderTypeId);
        Task<bool>Add(OrderTypeViewModel orderTypesViewModel);
        Task<bool> Update(OrderTypeViewModel orderTypesViewModel);
    }
}
