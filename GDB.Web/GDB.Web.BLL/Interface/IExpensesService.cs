using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface IExpensesService
    {
        Task<T?> GetAllExpenses<T>();
        Task<bool?> Add(ExpensesViewModel expensesViewModel);
        Task<bool?> Update(ExpensesViewModel expensesViewModel);
        Task<T?> GetTotalExpensesByWeekwise<T>();
    }
}
