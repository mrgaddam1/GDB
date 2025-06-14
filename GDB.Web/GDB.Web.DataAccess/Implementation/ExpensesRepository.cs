﻿using GDB.Web.Common.Extensions;
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
            var expensesByCurrentWeek = new List<ExpensesViewModel>();
           
 

                try
                {
                    var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_CurrentWeek", null);
                    expensesByCurrentWeek = ConvertDataTableToGenericList.ConvertDataTable<ExpensesViewModel>(data).
                                       OrderByDescending(x => x.WeekNumber).ToList();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, "An error occured while processing the request.");
                    expensesByCurrentWeek = new List<ExpensesViewModel>();
                }
                return expensesByCurrentWeek;

                //expensesData = await (from e in DbContext.Expenses
                //                      join g in DbContext.Groceries
                //                      on e.GroceryId equals g.GroceryId
                //                      join s in DbContext.Stores
                //                      on e.StoreId equals s.StoreId
                //                      select new ExpensesViewModel
                //                      {
                //                          ExpensesId = e.ExpensesId,                                          
                //                          GroceryDescription = g.GroceryDescription,
                //                          QuantityDescription = e.QuantityDescription ?? null,
                //                          StoreName = s.StoreName,
                //                          ExpensesAmount = e.Amount,
                //                          ExpensesDate = e.ExpensesDate,
                //                          WeekNumber = e.WeekId

                //                      }).OrderByDescending(x => x.WeekNumber).ToListAsync();
            
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


        //Expesnes Reports....
        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Weekwise()
        {
            var expensesByWeekData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_Weekly", null);
                expensesByWeekData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByWeekData;
        }

        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_BIWeekly()
        {
            var expensesByBIWeekData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_BiWeekly", null);
                expensesByBIWeekData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByBIWeekData;
        }
        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Monthly()
        {
            var expensesByMonthData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_Monthly", null);
                expensesByMonthData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByMonthData;
        }
        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Quarterly()
        {
            var expensesByQuarterlyData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_Quarterly", null);
                expensesByQuarterlyData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByQuarterlyData;
        }
        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_HalfYearly()
        {
            var expensesByHalfYearlyData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_Halfyearly", null);
                expensesByHalfYearlyData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByHalfYearlyData;
        }
        public async Task<List<ExpensesReportViewModel>> GetExpesesReportBy_Yearly()
        {
            var expensesByYearlyData = new List<ExpensesReportViewModel>();

            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Expesnes_GetAllExpensesBy_Yearly", null);
                expensesByYearlyData = ConvertDataTableToGenericList.ConvertDataTable<ExpensesReportViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesByYearlyData;
        }
    }
}
