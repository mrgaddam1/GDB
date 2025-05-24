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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ILogger<OrdersRepository> logger;  
        private GDBContext DbContext { get; set; }

        public OrdersRepository(GDBContext _DbContext, ILogger<OrdersRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }

        public async Task<int?> GetMaxWeekId()
        {
            int? maxWeekId;           
            DateTime todaysDate = DateTime.Now;
            DateTime weekDate = (from w in DbContext.WeekData select w.WeekDate.Value).FirstOrDefault();
            int numberOfDays = (todaysDate - weekDate).Days;

            var weekData = await (DbContext.WeekData.OrderByDescending(x => x.WeekId).FirstOrDefaultAsync());

            //if (numberOfDays > 0)
            if (numberOfDays > 7)
            {               
                weekData.WeekNumber = weekData.WeekNumber.Value + 1;
                weekData.WeekDate = System.DateTime.Now;
                
                DbContext.WeekData.Update(weekData);
                DbContext.SaveChanges();

                maxWeekId = weekData.WeekNumber;
            }
            else
            {
                maxWeekId = weekData.WeekNumber.Value;
            }
            
            return maxWeekId;
        }
        public async Task<List<OrdersViewModel>> GetAllOrders()
        {
            var ordersData = new List<OrdersViewModel>();
            ordersData = await (from o in DbContext.Orders
                                join c in DbContext.Customers on o.CustomerId equals c.CustomerId
                                join a in DbContext.AdvertiseSources on c.AdvertiseSourceId equals a.AdvertiseId
                                join l in DbContext.Locations on c.LocationId equals l.LocationId
                                
                                join p in DbContext.PaymentTypes on o.PaymentTypeId equals p.PaymentTypeId into paymentGroup
                                from p in paymentGroup.DefaultIfEmpty()

                                join ot in DbContext.OrderTypes on o.OrderTypeId equals ot.OrderTypeId into orderGroup
                                from ot in orderGroup.DefaultIfEmpty()

                                select new OrdersViewModel
                                {
                                    OrderId = o.OrderId,
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
        public async Task<bool> Add(OrdersViewModel ordersViewModel)
        {
            string message = string.Empty;
            bool orderStatus = false;

            try
            {
              
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", 1),
                    new SqlParameter("@WeekId", ordersViewModel.WeekId),
                    new SqlParameter("@CustomerId", ordersViewModel.CustomerId),
                    new SqlParameter("@StaterId", ordersViewModel.StaterId),
                    new SqlParameter("@StaterQuantity", ordersViewModel.StaterQuantity),
                    new SqlParameter("@TotalStaterPrice", ordersViewModel.SelectedStaterPrice),
                    new SqlParameter("@OrderTypeId", ordersViewModel.OrderTypeId),
                    new SqlParameter("@OrderDate", ordersViewModel.OrderDate),
                    new SqlParameter("@Quantity", ordersViewModel.Quantity),
                    new SqlParameter("@Amount", ordersViewModel.Amount),
                   
                };

                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Orders_Save_Orders", sqlParameters.ToArray());

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


        public async Task<bool> Update(OrdersViewModel ordersViewModel)
        {
            try
            {
                var order = (DbContext.Orders.SingleOrDefault(x => x.OrderId == ordersViewModel.OrderId));

                if (order != null)
                {
                   order.PaymentTypeId = ordersViewModel.PaymentTypeId;
                   order.AmountPaid = ordersViewModel.AmountPaid;
                   order.AmountPaidDate = ordersViewModel.AmountPaidDate;
                   order.ModifiedDate = System.DateTime.Now;
                }
                 
                DbContext.Orders.Update(order);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }



        public async Task<List<OrdersReportViewModel>> GetAllOrdersWeekly()
        {
            var ordersWeeklyData = new List<OrdersReportViewModel>();
             
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_Weekly", null);
                ordersWeeklyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }         
            return ordersWeeklyData;
        }

        public async Task<List<OrdersReportViewModel>> GetAllOrdersByWeekly()
        {
            var ordersBIWeeklyData = new List<OrdersReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_By_Weekly", null);
                ordersBIWeeklyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ordersBIWeeklyData;
        }

        public async Task<List<OrdersReportViewModel>> GetAllOrdersByMonthly()
        {
            var ordersMonthlyData = new List<OrdersReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_Monthly", null);
                ordersMonthlyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ordersMonthlyData;
        }

        public async Task<List<OrdersReportViewModel>> GetAllOrdersByQuarterly()
        {
            var ordersQuarterlyData = new List<OrdersReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_Quarterly", null);
                ordersQuarterlyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ordersQuarterlyData;
        }

        public async Task<List<OrdersReportViewModel>> GetAllOrdersByHalfYearly()
        {
            var ordersHalyYearlyData = new List<OrdersReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_HalfYearly", null);
                ordersHalyYearlyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ordersHalyYearlyData;
        }
        public async Task<List<OrdersReportViewModel>> GetAllOrdersByYearly()
        {
            var ordersYearlyData = new List<OrdersReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_Orders_GetAllSales_Yearly", null);
                ordersYearlyData = ConvertDataTableToGenericList.ConvertDataTable<OrdersReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ordersYearlyData;
        }
       

        public async Task<List<TotalOrdersByWeekViewModel>> GetTotalOrdersByWeekwise()
        {
            var totalOrdersByWeekViewModel = new List<TotalOrdersByWeekViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAll_TotalOrders_By_WeekWise", null);
                totalOrdersByWeekViewModel = ConvertDataTableToGenericList.ConvertDataTable<TotalOrdersByWeekViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return totalOrdersByWeekViewModel;
        }

       



    }
}
