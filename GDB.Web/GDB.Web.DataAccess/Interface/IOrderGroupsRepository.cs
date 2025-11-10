using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IOrderGroupsRepository
    {
        Task<List<OrderGroupsViewModel>> GetAllOrders();
        Task<bool>Add(OrderGroupsViewModel orderGroupsViewModel);
        Task<bool> Update(OrderGroupsViewModel orderGroupsViewModel);
      
    }
}
