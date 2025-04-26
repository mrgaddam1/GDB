using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryViewModel>> GetAll<T>();
        Task<bool?> Add(SubCategoryViewModel subCategoryViewModel);
        Task<bool?> Update(SubCategoryViewModel subCategoryViewModel);
    }
}
