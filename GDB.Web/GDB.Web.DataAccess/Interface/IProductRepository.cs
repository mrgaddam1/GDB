using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductViewModel>> GetAll();
        Task<bool> Add(ProductViewModel productViewModel);
        Task<bool> Update(ProductViewModel productViewModel);
    }
}
