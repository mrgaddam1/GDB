using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class OrderService : IOrderService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public OrderService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<T?> GetAllOrders<T>()
        {
            var response = await httpClient.GetAsync("api/Orders/GetAllOrders");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
        public async Task<bool?> Add(OrdersViewModel ordersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Orders/Add", ordersViewModel);
                //return await ApiStatusCodeHandler.HandleResponse<bool>(response);
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

        public async Task<T?> GetMaxWeekId<T>()
        {
            var response = await httpClient.GetAsync("api/Orders/GetMaxWeekId");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<bool?> Update(OrdersViewModel ordersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Orders/Update", ordersViewModel);
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

        public async Task<T?> GetAllOrdersWeekly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByWeekly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllOrdersByWeekly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByBIWeekly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllOrdersByMonthly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByMonthly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllOrdersByQuarterly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByQuarterly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllOrdersByHalfYearly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByHalfYearly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetAllOrdersByYearly<T>()
        {
            var response = await httpClient.GetAsync("api/OrderReports/GetAllOrderByYearly");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetTotalOrdersByWeekwise<T>()
        {
            var response = await httpClient.GetAsync("api/Orders/GetTotalOrdersByWeekwise");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
    }
}
