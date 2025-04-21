using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ILogger<ExpensesRepository> logger;
        private GDBContext DbContext { get; set; }

        public ExpensesRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async  Task<List<ExpensesViewModel>> GetAllExpenses()
        {
            var expensesData = new List<ExpensesViewModel>();
            try
            {
                expensesData = await (from e in DbContext.Expenses
                                      join g in DbContext.Groceries
                                      on e.GroceryId equals g.GroceryId
                                      join s in DbContext.Stores
                                      on e.StoreId equals s.StoreId
                                      select new ExpensesViewModel
                                      {
                                          ExpensesId = e.ExpensesId,
                                          UserId = e.UserId,
                                          GroceryDescription = g.GroceryDescription,
                                          QuantityDescription = e.QuantityDescription ?? null,
                                          StoreName = s.StoreName,
                                          ExpensesAmount = e.Amount,
                                          ExpensesDate = e.ExpensesDate,
                                          WeekNumber = e.WeekId

                                      }).OrderByDescending(x => x.WeekNumber).ToListAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                expensesData = new List<ExpensesViewModel>();
            }           

            return expensesData;
        }
        public async Task<bool> Add(ExpensesViewModel expensesViewModel)
        {
            try
            {
                var expense = new Expense
                {
                    UserId = 1,
                    GroceryId = expensesViewModel.GroceryId,
                    StoreId = expensesViewModel.StoreId,
                    Amount = expensesViewModel.ExpensesAmount,
                    ExpensesDate= expensesViewModel.ExpensesDate,
                    WeekId = expensesViewModel.WeekNumber,
                    QuantityDescription = expensesViewModel.QuantityDescription,
                };
                DbContext.Expenses.Add(expense);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }

        }
        public async Task<bool> Update(ExpensesViewModel expensesViewModel)
        {
            try
            {
                var existingExpensesData = (DbContext.Expenses.SingleOrDefault(x => x.ExpensesId == expensesViewModel.ExpensesId));

                if (existingExpensesData != null)
                {
                    existingExpensesData.UserId = 1;               
                    existingExpensesData.QuantityDescription = expensesViewModel.QuantityDescription;                
                    existingExpensesData.Amount = expensesViewModel.ExpensesAmount;
                    existingExpensesData.ExpensesDate = expensesViewModel.ExpensesDate;
                    existingExpensesData.ModifiedDate =  System.DateTime.Now;
                }

                DbContext.Expenses.Update(existingExpensesData);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<TotalExpensesByWeekViewModel>> GetTotalExpensesByWeekwise()
        {
            var TotalExpensesByWeekViewModel = new List<TotalExpensesByWeekViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_GetAll_TotalExpenses_By_WeekWise", null);
                TotalExpensesByWeekViewModel = ConvertDataTableToGenericList.ConvertDataTable<TotalExpensesByWeekViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return TotalExpensesByWeekViewModel;
        }
    }
}
