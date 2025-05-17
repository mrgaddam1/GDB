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
    public class GroceryRepository : IGroceryRepository
    {
        private readonly ILogger<GroceryRepository> logger;
        private GDBContext DbContext { get; set; }

        public GroceryRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<GroceryViewModel>> GetAll()
        {
            var groceryData = new List<GroceryViewModel>();
            groceryData = await (from g in DbContext.Groceries.AsNoTracking()
                                 select new GroceryViewModel
                                 {
                                     GroceryId = g.GroceryId,
                                     GroceryDescription = g.GroceryDescription,
                                 }).OrderBy(x => x.GroceryDescription).ToListAsync();
            return groceryData;
        }

        public async Task<bool> Add(GroceryViewModel groceryViewModel)
        {
            try
            {
                var grocery = new Grocery
                {
                    UserId = 1,
                    GroceryDescription = groceryViewModel.GroceryDescription,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
                DbContext.Groceries.Add(grocery);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> Update(GroceryViewModel groceryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var existingGroceryData = (DbContext.Groceries.SingleOrDefault(x => x.GroceryId == groceryViewModel.GroceryId));

                if (existingGroceryData == null)
                {
                    logger.LogWarning("Grocery with ID {GroceryId} not found.", groceryViewModel.GroceryId);
                    isSuccess =false;
                }
                if (existingGroceryData != null)
                {
                    existingGroceryData.UserId = 1;
                    existingGroceryData.GroceryDescription = groceryViewModel.GroceryDescription;
                    existingGroceryData.ModifiedDate = DateTime.UtcNow;
                    DbContext.Entry(existingGroceryData).State = EntityState.Modified;
                    await DbContext.SaveChangesAsync();
                    isSuccess= true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                isSuccess =false;
            }
            return isSuccess;
        }
    }
}
