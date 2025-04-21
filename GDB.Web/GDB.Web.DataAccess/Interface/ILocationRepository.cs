using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface ILocationRepository
    {
        Task<List<LocationViewModel>> GetAllLocations();
        Task<bool> Add(LocationViewModel locationViewModel);
        Task<bool> Update(LocationViewModel locationViewModel);
    }
}
