using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IStaterRepository
    {
        Task<List<StatersViewModel>> GetAllStaters();
        Task<decimal>GetStaterPriceByStater(int staterId);
        Task<bool> Add(StatersViewModel statersViewModel);
        Task<bool> Update(StatersViewModel statersViewModel);
    }
}
