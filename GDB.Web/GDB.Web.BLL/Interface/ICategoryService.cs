using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll<T>();
        Task<bool?> Add(CategoryViewModel categoryViewModel);
        Task<bool?> Update(CategoryViewModel categoryViewModel);
    }
}
