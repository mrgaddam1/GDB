using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IStoreService
    {
        Task<List<StoreViewModel>> GetAll<T>();
        Task<bool?> Add(StoreViewModel storeViewModel);
        Task<bool?> Update(StoreViewModel storeViewModel);
    }
}
