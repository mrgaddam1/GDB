using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GDB.Web.DataAccess.Implementation
{
    public class OrderGroupsRepository : IOrderGroupsRepository
    {
        private readonly ILogger<OrderGroupsRepository> logger;  
        private GDBContext DbContext { get; set; }

        public OrderGroupsRepository(GDBContext _DbContext, ILogger<OrderGroupsRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
                
        public async Task<List<OrderGroupsViewModel>> GetAllOrders()
        {
            var ordersData = new List<OrderGroupsViewModel>();
            ordersData = await (from o in DbContext.OrderGroups
                                join c in DbContext.Customers on o.CustomerId equals c.CustomerId
                                join a in DbContext.AdvertiseSources on c.AdvertiseSourceId equals a.AdvertiseId
                                join l in DbContext.Locations on c.LocationId equals l.LocationId
                                
                                join p in DbContext.PaymentTypes on o.PaymentTypeId equals p.PaymentTypeId into paymentGroup
                                from p in paymentGroup.DefaultIfEmpty()

                                join ot in DbContext.OrderTypes on o.OrderTypeId equals ot.OrderTypeId into orderGroup
                                from ot in orderGroup.DefaultIfEmpty()

                                select new OrderGroupsViewModel
                                {
                                    OrderGroupId = o.OrderGroupId,
                                    CustomerName = c.FirstName + "  " + c.LastName,
                                    LastName = c.LastName,
                                    MobileNumber = c.MobileNumber,
                                    OrderTypeName = ot.OrderTypeName,
                                    Quantity = o.Quantity,
                                    AdvertisementDescription = a.AdvertiseDescription,
                                    Location = l.LocationDescription,
                                    Amount = o.Amount,
                                    AmountPaid = o.AmountPaid,
                                    AmountPaidDate = o.AmountPaidDate.Value.Date,
                                    OrderDate = o.OrderDate.Value.Date,
                                    PaymentTypeId = o.PaymentTypeId,
                                    PaymentType = p.PaymentTypeDescription,
                                    WeekId = o.WeekId,
                                    FoodPackingTypeId = o.FoodPackingTypeId,


                                }).OrderByDescending(x=>x.WeekId)
                                  .ThenBy(x => x.AmountPaid == false || x.AmountPaid == null)
                                  .ThenBy(x => x.AmountPaid == true)

                                .ToListAsync();
            return ordersData;
        }
        public async Task<bool> Add(OrderGroupsViewModel orderGroupsViewModel)
        {
            string message = string.Empty;
            bool orderStatus = false;

            try
            {              
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", 1),
                    new SqlParameter("@WeekId", orderGroupsViewModel.WeekId),
                    new SqlParameter("@CustomerId", orderGroupsViewModel.CustomerId),
                    new SqlParameter("@StaterId", orderGroupsViewModel.StaterId),
                    new SqlParameter("@StaterQuantity", orderGroupsViewModel.StaterQuantity),
                    new SqlParameter("@TotalStaterPrice", orderGroupsViewModel.SelectedStaterPrice),
                    new SqlParameter("@OrderTypeId", orderGroupsViewModel.OrderTypeId),
                    new SqlParameter("@OrderDate", orderGroupsViewModel.OrderDate),
                    new SqlParameter("@Quantity", orderGroupsViewModel.Quantity),
                    new SqlParameter("@Amount", orderGroupsViewModel.Amount),
                   
                };

                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_OrderGroups_Save_Orders", sqlParameters.ToArray());

                if (data.Rows.Count > 0)
                {
                    message = data.Rows[0]["Transaction_Message"].ToString();
                    if (message == "Data inserted successfully.")
                    {
                        logger.LogInformation(message);
                        orderStatus = true;
                    }
                    else if (message == "Product Available quantity is 0 - Data inserted successfully.")
                    {
                        logger.LogInformation(message);
                        orderStatus = true;
                    }
                    else if (message == "Unable to process transaction.")
                    {
                        logger.LogInformation(message);
                        orderStatus = false;
                    }
                    else
                    {
                        logger.LogInformation(message);
                        orderStatus = false;
                    }
                }
                return orderStatus;

            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message,"An error occured while processing the request.");
                return orderStatus;
            }            
        }


        public async Task<bool> Update(OrderGroupsViewModel orderGroupsViewModel)
        {
            try
            {
                var order = (DbContext.OrderGroups.SingleOrDefault(x => x.OrderGroupId == orderGroupsViewModel.OrderGroupId));

                if (order != null)
                {
                   order.PaymentTypeId = orderGroupsViewModel.PaymentTypeId;
                   order.AmountPaid = orderGroupsViewModel.AmountPaid;
                   order.AmountPaidDate = orderGroupsViewModel.AmountPaidDate;
                   order.ModifiedDate = System.DateTime.Now;
                }
                 
                DbContext.OrderGroups.Update(order);
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
