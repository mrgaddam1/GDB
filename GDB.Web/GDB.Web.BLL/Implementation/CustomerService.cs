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
    public class CustomerService : ICustomerService
    {
        public HttpClient httpClient { get; set; }

        public CustomerService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<T?> GetAllCustomers<T>()
        {
            var response = await httpClient.GetAsync("api/Customers/GetAllCustomers");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<bool?> Add(CustomerViewModel customerViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Customers/Add", customerViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                    isSuccess = false;
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

        public async Task<bool?> Update(CustomerViewModel customerViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Customers/Update", customerViewModel);
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

        public async Task<T?> GetAllCustomerVisits<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisits");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_Weekly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsWeekly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_BIWeekly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsByWeekly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_Monthly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsByMonthly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_Quarterly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsQuarterly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_HalyYearly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsHalfYearly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllCustomerVisitsBy_Yearly<T>()
        {
            var response = await httpClient.GetAsync("api/CustomerReports/GetAllCustomerVisitsYearly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

       
    }
}

