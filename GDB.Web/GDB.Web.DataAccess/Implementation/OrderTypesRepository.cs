using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderTypesRepository> logger;

        public OrderTypesRepository(GDBContext _DbContext, ILogger<OrderTypesRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<List<OrdersTypeViewModel>> GetAllOrderTypes()
        {
             var orderTypesData = new List<OrdersTypeViewModel>();
            orderTypesData = await(from o in DbContext.OrderTypes join p in DbContext.FoodPackingTypes
                                   on o.FoodPackingTypeId equals p.FoodPackingTypeId
                                  select new OrdersTypeViewModel
                                  {
                                      OrderTypeId = o.OrderTypeId,
                                      OrderTypeName = o.OrderTypeName,
                                      FoodPackingTypeId = o.FoodPackingTypeId,
                                      FoodPackingTypeDescription  = p.FoodPackingTypeDescription
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

        public async Task<bool> Add(OrdersTypeViewModel ordersTypeViewModel)
        {
            try
            {
                var orderTypes = new OrderType
                {
                    UserId = 1,
                    OrderTypeName = ordersTypeViewModel.OrderTypeName,
                    FoodPackingTypeId = ordersTypeViewModel.FoodPackingTypeId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null                    
                };  
               
                DbContext.OrderTypes.Add(orderTypes);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }
        public async Task<bool> Update(OrdersTypeViewModel ordersTypeViewModel)
        {
            try
            {
                var existingOrderTypesData = (DbContext.OrderTypes.SingleOrDefault(x => x.OrderTypeId == ordersTypeViewModel.OrderTypeId));

                if (existingOrderTypesData == null)
                {
                    logger.LogWarning("Order Type with ID {OrderType Id} not found.", ordersTypeViewModel.OrderTypeId);
                    return false;
                }
                if (existingOrderTypesData != null)
                {
                    existingOrderTypesData.OrderTypeName = ordersTypeViewModel.OrderTypeName;
                    existingOrderTypesData.UserId = 1;
                    existingOrderTypesData.ModifiedDate = DateTime.UtcNow;
                }

                DbContext.OrderTypes.Update(existingOrderTypesData);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }     
    }
}
