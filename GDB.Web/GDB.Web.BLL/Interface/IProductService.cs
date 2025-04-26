using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAll<T>();
        Task<bool?> Add(ProductViewModel productViewModel);
        Task<bool?> Update(ProductViewModel productViewModel);
    }
}
