﻿using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
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
            try
            {
                var inventory = new Inventory
                {
                    UserId = 1,
                    ProductId = inventoryViewModel.ProductId,
                    WeekId = inventoryViewModel.WeekId,
                    Quantity = inventoryViewModel.Quantity,
                    AvailableQuantity = inventoryViewModel.AvailableQuantity,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
                DbContext.Inventories.Add(inventory);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<InventoryViewModel>> GetAll()
        {
            var inventoryListData = new List<InventoryViewModel>();
            try
            {
                inventoryListData = await (from i in DbContext.Inventories 
                                           join p in DbContext.Products on i.ProductId equals p.ProductId
                                           join f in DbContext.FoodPackingTypes on p.FoodPackingTypeId equals f.FoodPackingTypeId
                                           select new InventoryViewModel
                                           {
                                               InventoryId = i.InventoryId,
                                               ProductName = p.ProductName,                                             
                                               Quantity = i.Quantity,
                                               AvailableQuantity = i.AvailableQuantity,
                                               FoodPackingTypeDescription = f.FoodPackingTypeDescription
                                               
                                           }).OrderBy(x=>x.InventoryId).ToListAsync();
                return inventoryListData;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all Inventories.");
                return new List<InventoryViewModel>();
            }
        }

        public async Task<bool> Update(InventoryViewModel inventoryViewModel)
        {
            var isUpdated = false;
            try
            {
                var inventory = await DbContext.Inventories.FindAsync(inventoryViewModel.InventoryId);
                if (inventory != null)
                {
                    if(inventoryViewModel.NewStockQuantity == 0 || inventoryViewModel.NewStockQuantity == null)
                    {
                        inventory.Quantity = inventoryViewModel.Quantity;
                        inventory.AvailableQuantity = inventoryViewModel.AvailableQuantity;
                    }
                    else
                    {
                        inventory.Quantity = (inventoryViewModel.NewStockQuantity + inventory.Quantity);
                        inventory.AvailableQuantity = (inventoryViewModel.NewStockQuantity + inventory.AvailableQuantity);
                    }
                    
                    inventory.ModifiedDate = DateTime.UtcNow;
                    inventory.UserId = 1;
                    DbContext.Inventories.Update(inventory);
                    await DbContext.SaveChangesAsync();
                    isUpdated =  true;
                }
                return isUpdated;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the Inventory.");
                return isUpdated;
            }
        }

        public async Task<List<InventoryHistoryViewModel>> GetAllInventoryHistory()
        {
            var inventoryHistoryData = new List<InventoryHistoryViewModel>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Inventory_Inventory_History_Details", null);
                inventoryHistoryData = ConvertDataTableToGenericList.ConvertDataTable<InventoryHistoryViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all Inventories.");
                return new List<InventoryHistoryViewModel>();
            }
            return inventoryHistoryData;
        }


    }
}
