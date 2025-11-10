using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IOrderGroupService
    {
        Task<T?> GetAllOrders<T>();
        Task<bool?> Add(OrderGroupsViewModel orderGroupsViewModel);
        Task<bool?> Update(OrderGroupsViewModel orderGroupsViewModel);
        
    }
}
