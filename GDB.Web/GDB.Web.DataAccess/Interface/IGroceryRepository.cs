using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IGroceryRepository
    {
        Task<List<GroceryViewModel>> GetAll();
        Task<bool> Add(GroceryViewModel groceryViewModel);
        Task<bool> Update(GroceryViewModel groceryViewModel);
    }
}
