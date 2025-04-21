using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IExpensesRepository
    {
        Task<List<ExpensesViewModel>> GetAllExpenses();
        Task<bool> Add(ExpensesViewModel expensesViewModel);
        Task<bool> Update(ExpensesViewModel expensesViewModel);
        Task<List<TotalExpensesByWeekViewModel>> GetTotalExpensesByWeekwise();
    }
}
