using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ILogger<InventoryRepository> logger;
        private GDBContext DbContext { get; set; }

        public InventoryRepository(GDBContext _DbContext, ILogger<InventoryRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<bool> Add(InventoryViewModel inventoryViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InventoryViewModel>> GetAll()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all Inventories.");
                return new List<InventoryViewModel>();
            }
        }

        public async Task<bool> Update(InventoryViewModel inventoryViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(int inventoryId)
        {
            throw new NotImplementedException();
        }
    }
}
