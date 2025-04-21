using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> logger;
        private GDBContext DbContext { get; set; }

        public CustomerRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<CustomerViewModel>> GetAll()
        {
            var customersData = new List<CustomerViewModel>();
            customersData = await (from c in DbContext.Customers.AsNoTracking()
                                   join l in DbContext.Locations.AsNoTracking()
                                   on c.LocationId equals l.LocationId
                                   join a in DbContext.AdvertiseSources
                                   on c.AdvertiseSourceId equals a.AdvertiseId
                                   select new CustomerViewModel
                                   {
                                       CustomerId = c.CustomerId,
                                       FirstName = c.FirstName,
                                       LastName = c.LastName,
                                       MobileNumber = c.MobileNumber,
                                       LocationName = l.LocationDescription,
                                       AdvertiseSource = a.AdvertiseDescription
                                   }).OrderBy(x => x.FirstName).ToListAsync();
            return customersData;

        }

        public async Task<bool> Add(CustomerViewModel customerViewModel)
        {
            try
            {
                var customer = new Customer
                {
                    UserId = 1,
                    FirstName = customerViewModel.FirstName,
                    LastName = customerViewModel.LastName,
                    MobileNumber = customerViewModel.MobileNumber,
                    LocationId = customerViewModel.locationId,
                    AdvertiseSourceId = customerViewModel.AdvertiseSourceId,
                    CreatedDate = DateTime.Now,
                };
                DbContext.Customers.Add(customer);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> Update(CustomerViewModel customerViewModel)
        {
            try
            {
                var existingCustomerData = (DbContext.Customers.SingleOrDefault(x => x.CustomerId == customerViewModel.CustomerId));
              
                if (existingCustomerData != null)
                {
                    existingCustomerData.FirstName = customerViewModel.FirstName;
                    existingCustomerData.LastName = customerViewModel.LastName;
                    existingCustomerData.MobileNumber = customerViewModel.MobileNumber;                    
                    existingCustomerData.ModifiedDate = DateTime.Now;
                }
              
                DbContext.Customers.Update(existingCustomerData);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisits()
        {
            var customerVisits = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits", null);
                customerVisits = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisits;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Weekly()
        {
            var customerVisitsWeekly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_Weekly", null);
                customerVisitsWeekly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsWeekly;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_BIWeekly()
        {
            var customerVisitsByWeekly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_Bi_Weekly", null);
                customerVisitsByWeekly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsByWeekly;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Monthly()
        {
            var customerVisitsMonthly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_Bi_Monthly", null);
                customerVisitsMonthly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsMonthly;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Quarterly()
        {
            var customerVisitsQuarterly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_By_Quarterly", null);
                customerVisitsQuarterly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsQuarterly;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_HalyYearly()
        {
            var customerVisitsHalfYearly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_By_HalfYearly", null);
                customerVisitsHalfYearly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsHalfYearly;
        }

        public async Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Yearly()
        {
            var customerVisitsYearly = new List<CustomerVisits>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAllCustomerVisits_By_Yearly", null);
                customerVisitsYearly = ConvertDataTableToGenericList.ConvertDataTable<CustomerVisits>(data).
                                   OrderByDescending(x => x.NumberofVisits).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return customerVisitsYearly;
        }
    }
}
