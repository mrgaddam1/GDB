using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IPaymentTypeService
    {
        Task<T?> GetAllPaymentTypes<T>();
    }
}
