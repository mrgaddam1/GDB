using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IStaterService
    {
        Task<T?> GetAllStaters<T>();
        Task<decimal> GetStaterPriceByStater(int staterId);
    }
}
