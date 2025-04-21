using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IGroceryService
    {
        Task<List<GroceryViewModel>> GetAll<T>();
    }
}
