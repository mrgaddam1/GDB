using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<bool> Add(CategoryViewModel categoryViewModel);
        Task<bool> Update(CategoryViewModel categoryViewModel);
    }
}
