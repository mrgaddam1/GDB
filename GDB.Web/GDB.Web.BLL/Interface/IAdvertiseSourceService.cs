using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IAdvertiseSourceService
    {
        Task<T?> GetAllAdvertiseSourceDetails<T>();
        Task<bool?> Add(AdvertiseSourceViewModel advertiseSourceViewModel);
        Task<bool?> Update(AdvertiseSourceViewModel advertiseSourceViewModel);
    }
}
