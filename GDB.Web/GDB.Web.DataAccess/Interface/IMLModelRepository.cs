using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IMLModelRepository
    {
        Task<List<OrderPredictions>> TrainModelAndPredictSales();
        Task<List<ExpensesPredictions>> TrainModelAndPredictExpenses();
        Task<List<OrderPredictions>> GetExistingSalesData();
        Task<List<ExpensesPredictions>> GetExistingExpensesData();
    }
}
