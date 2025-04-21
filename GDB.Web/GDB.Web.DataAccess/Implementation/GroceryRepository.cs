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
                                     }).OrderBy(x=>x.GroceryDescription).ToListAsync();
            return groceryData;
        }
    }
}
