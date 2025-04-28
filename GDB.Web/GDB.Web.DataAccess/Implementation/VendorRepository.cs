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
    public class VendorRepository : IVendorRepository
    {
        private readonly ILogger<VendorRepository> logger;
        private GDBContext DbContext { get; set; }

        public VendorRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<VendorViewModel>> GetAll()
        {
            var vendors = new List<VendorViewModel>();
            try
            {

                vendors = await (from s in DbContext.Vendors.AsNoTracking()
                                 select new VendorViewModel
                                 {
                                     VendorId = s.VendorId,
                                     VendorName = s.VendorName
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
    }
}
