using GDB.Web.Common.Helpers;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ILogger<VendorRepository> logger;
        private GDBContext DbContext { get; set; }

        public VendorRepository(GDBContext _DbContext, ILogger<VendorRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<List<VendorViewModel>> GetAll()
        {
            var vendors = new List<VendorViewModel>();
            try
            {

                vendors = await (from s in DbContext.Vendors.AsNoTracking()
                                 join a in DbContext.VendorAddresses.AsNoTracking() on s.VendorId equals a.VendorId 
                                 join c in DbContext.VendorContacts.AsNoTracking() on s.VendorId equals c.VendorId                                 
                                 select new VendorViewModel
                                 {
                                     VendorId = s.VendorId,
                                     VendorName = s.VendorName,
                                     FirstName = c.FirstName,
                                     LastName = c.LastName,
                                     ContactNumber = c.ContactNumber,
                                     AddressLine1 = a.AddressLine1,
                                     AddressLine2 = a.AddressLine2,
                                     Postcode = a.Postcode,
                                     VendorAddressId = a.VendorAddressId,
                                     VendorContactId = c.VendorContactId

                                 }).OrderBy(x => x.VendorName).ToListAsync();
                if (vendors == null || vendors.Count == 0)
                {
                    logger.LogInformation("No Vendors found in the database.");
                    return new List<VendorViewModel>();
                }
                return vendors;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all locations.");
                return new List<VendorViewModel>();
            }
        }
        public async Task<bool> Add(VendorViewModel vendorViewModel)
        {
           
            string message = string.Empty;
            bool vendorStatus = false;

            try
            {

                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@VendorName", vendorViewModel.VendorName),
                    new SqlParameter("@FirstName", vendorViewModel.FirstName),
                    new SqlParameter("@LastName", vendorViewModel.LastName),
                    new SqlParameter("@ContactNumber", vendorViewModel.ContactNumber),
                    new SqlParameter("@AddressLine1", vendorViewModel.AddressLine1),
                    new SqlParameter("@AddressLine2", vendorViewModel.AddressLine2),
                    new SqlParameter("@Postcode", vendorViewModel.Postcode),

                };

                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Vendors_Save_Vendor_Details", sqlParameters.ToArray());

                if (data.Rows.Count > 0)
                {
                    message = data.Rows[0]["Transaction_Message"].ToString();
                    if (message == "Data inserted successfully")
                    {
                        logger.LogInformation(message);
                        vendorStatus = true;
                    }                  
                    else if (message == "Unable to process transaction.")
                    {
                        logger.LogInformation(message);
                        vendorStatus = false;
                    }
                    else
                    {
                        logger.LogInformation(message);
                        vendorStatus = false;
                    }
                }
                return vendorStatus;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return vendorStatus;
            }


        }

        public async Task<bool> Update(VendorViewModel vendorViewModel)
        {
            string message = string.Empty;
            bool vendorStatus = false;

            try
            {

                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@VendorId", vendorViewModel.VendorId),
                    new SqlParameter("@VendorName", vendorViewModel.VendorName),
                    new SqlParameter("@FirstName", vendorViewModel.FirstName),
                    new SqlParameter("@LastName", vendorViewModel.LastName),
                    new SqlParameter("@ContactNumber", vendorViewModel.ContactNumber),
                    new SqlParameter("@AddressLine1", vendorViewModel.AddressLine1),
                    new SqlParameter("@AddressLine2", vendorViewModel.AddressLine2),
                    new SqlParameter("@Postcode", vendorViewModel.Postcode),
                    new SqlParameter("@VendorAddressId", vendorViewModel.VendorAddressId),
                    new SqlParameter("@VendorContactId", vendorViewModel.VendorContactId),

                };

                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Vendors_Update_Vendor_Details", sqlParameters.ToArray());

                if (data.Rows.Count > 0)
                {
                    message = data.Rows[0]["Transaction_Message"].ToString();
                    if (message == "Data updated successfully")
                    {
                        logger.LogInformation(message);
                        vendorStatus = true;
                    }
                    else if (message == "Unable to process transaction.")
                    {
                        logger.LogInformation(message);
                        vendorStatus = false;
                    }
                    else
                    {
                        logger.LogInformation(message);
                        vendorStatus = false;
                    }
                }
                return vendorStatus;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return vendorStatus;
            }
        }  
        
        public async Task<bool> Delete(int vendorId)
        {
            try
            {
                var existingVendorData = (DbContext.Vendors.SingleOrDefault(x => x.VendorId == vendorId));
                if (existingVendorData == null)
                {
                    logger.LogWarning("Vendor with ID {VendorId} not found.", vendorId);
                    return false;
                }
                DbContext.Vendors.Remove(existingVendorData);
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
