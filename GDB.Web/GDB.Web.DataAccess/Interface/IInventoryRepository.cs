using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IInventoryRepository
    {
        Task<List<InventoryViewModel>> GetAll();
        Task<bool> Add(InventoryViewModel inventoryViewModel);
        Task<bool> Update(InventoryViewModel inventoryViewModel);
    }
}
