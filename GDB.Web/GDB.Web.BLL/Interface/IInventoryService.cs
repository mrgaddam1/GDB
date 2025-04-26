using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IInventoryService
    {
        Task<List<InventoryViewModel>> GetAllInventoryStock<T>();
        Task<bool?> Add(InventoryViewModel inventoryViewModel);
        Task<bool?> Update(InventoryViewModel inventoryViewModel);
    }
}
