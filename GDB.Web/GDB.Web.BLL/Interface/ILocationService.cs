using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface ILocationService
    {
        Task<List<LocationViewModel>> GetAllLocations<T>();
        Task<bool?> Add(LocationViewModel locationViewModel);
        Task<bool?> Update(LocationViewModel locationViewModel);
    }
}
