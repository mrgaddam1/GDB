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
    public class StoreRepository : IStoreRepository
    {
        private readonly ILogger<StoreRepository> logger;
        private GDBContext DbContext { get; set; }

        public StoreRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<StoreViewModel>> GetAll()
        {
            var storeData = new List<StoreViewModel>();
            storeData = await (from s in DbContext.Stores.AsNoTracking()
                                 select new StoreViewModel
                                 {
                                     StoreId = s.StoreId,
                                     StoreName = s.StoreName
                                 }).OrderBy(x => x.StoreName).ToListAsync();
            return storeData;
        }
        public async Task<bool> Add(StoreViewModel storeViewModel)
        {
            try
            {
                var store = new Store
                {
                    UserId = 1,
                    StoreId = storeViewModel.StoreId,
                    StoreName = storeViewModel.StoreName,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                };
                DbContext.Stores.Add(store);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> Update(StoreViewModel storeViewModel)
        {
            try
            {
                var existingStoreData = (DbContext.Stores.SingleOrDefault(x => x.StoreId == storeViewModel.StoreId));

                if (existingStoreData != null)
                {
                    existingStoreData.StoreName = storeViewModel.StoreName;
                    existingStoreData.UserId = 1;
                    existingStoreData.ModifiedDate = System.DateTime.Now;
                }

                DbContext.Stores.Update(existingStoreData);
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
