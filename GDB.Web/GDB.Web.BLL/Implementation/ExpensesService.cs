using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class ExpensesService : IExpensesService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public ExpensesService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<T?> GetAllExpenses<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Expenses/GetAllExpenses");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
                return await ApiStatusCodeHandler.HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                return await ApiStatusCodeHandler.HandleResponse<T>(null);
            }          
            
        }

        public async Task<bool?> Add(ExpensesViewModel expensesViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Expenses/Add", expensesViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
                return isSuccess = true;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return isSuccess;
            }
        }

        public async Task<bool?> Update(ExpensesViewModel expensesViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Expenses/Update", expensesViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
                else
                {
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return isSuccess;
            }
        }

        public async Task<T?> GetTotalExpensesByWeekwise<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetTotalExpensesByWeekwise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_Weekwise<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByWeekWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_BIWeekly<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByBIWeeklyWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_Monthly<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByMonthWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_Quarterly<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByQuarterlyWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_HalfYearly<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByHalyYearlyWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T> GetExpesesReportBy_Yearly<T>()
        {
            var response = await httpClient.GetAsync("api/Expenses/GetAllExpensesByYearlyWise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
    }
}
