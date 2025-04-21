using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IMLModelService
    { 
        Task<T?> PredictSales<T>();
        Task<T?> PredictExpenses<T>();
        Task<T?> GetExistingSalesData<T>();
        Task<T?> GetExistingExpensesData<T>();
    }
}
