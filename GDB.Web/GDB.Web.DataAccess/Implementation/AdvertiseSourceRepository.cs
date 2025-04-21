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
    public class AdvertiseSourceRepository : IAdvertiseSourceRepository
    {
        private readonly ILogger<AdvertiseSourceRepository> logger;
        private GDBContext DbContext { get; set; }

        public AdvertiseSourceRepository(GDBContext _DbContext, ILogger<AdvertiseSourceRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<List<AdvertiseSourceViewModel>> GetAllAdvertiseSources()
        {
            var advertiseSourceData = new List<AdvertiseSourceViewModel>();
            advertiseSourceData = await (from a in DbContext.AdvertiseSources.AsNoTracking()
                                         select new AdvertiseSourceViewModel
                                         {
                                             AdvertiseId = a.AdvertiseId,
                                             AdvertiseDescription = a.AdvertiseDescription
                                         }).OrderByDescending(x => x.AdvertiseDescription).ToListAsync();
            return advertiseSourceData;
        }
    }
}
