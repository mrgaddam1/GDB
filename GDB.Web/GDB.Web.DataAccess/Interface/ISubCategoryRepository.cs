using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategoryViewModel>> GetAll();
        Task<bool> Add(SubCategoryViewModel subCategoryViewModel);
        Task<bool> Update(SubCategoryViewModel subCategoryViewModel);
    }
}
