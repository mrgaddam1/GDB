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
    public class OrderGroupService : IOrderGroupService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public OrderGroupService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<T?> GetAllOrders<T>()
        {
            var response = await httpClient.GetAsync("api/OrderGroups/GetAllOrders");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
        public async Task<bool?> Add(OrderGroupsViewModel ordersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/OrderGroups/Add", ordersViewModel);
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

        public async Task<bool?> Update(OrderGroupsViewModel ordersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/OrderGroups/Update", ordersViewModel);
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
    }
}
