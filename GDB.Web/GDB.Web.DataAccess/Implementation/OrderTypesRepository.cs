using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class OrderTypesRepository : IOrderTypesRepository
    {
        private GDBContext DbContext { get; set; }

        public OrderTypesRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<OrdersTypeViewModel>> GetAllOrderTypes()
        {
             var orderTypesData = new List<OrdersTypeViewModel>();
            orderTypesData = await(from o in DbContext.OrderTypes
                                  select new OrdersTypeViewModel
                                  {
                                      OrderTypeId = o.OrderTypeId,
                                      OrderTypeName = o.OrderTypeName
                                  }).OrderBy(x => x.OrderTypeName).ToListAsync();

            return orderTypesData;
        }
        public async Task<decimal?> GetSelectedOrderTypeItemPriceOrderType(int orderTypeId)
        {
            return await (from o in DbContext.OrderTypes
                          join ot in DbContext.OrderTypePrices
                          on o.OrderTypeId equals ot.OrderTypeId
                          where o.OrderTypeId == orderTypeId
                          select ot.OrderTypePrice1).SingleOrDefaultAsync();
        }
    }
}
