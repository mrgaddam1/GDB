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
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Weekwise();
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_BIWeekly();
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Monthly();
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Quarterly();
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_HalfYearly();
        Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Yearly();
    }
}
