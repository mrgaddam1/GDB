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
    public class OrderTypesService(HttpClient _httpClient) : IOrdersTypeService
    {
        public HttpClient httpClient { get; set; } = _httpClient;
        private string errorMessage;

        public async Task<T?> GetAllOrderTypes<T>()
        {
            var response = await httpClient.GetAsync("api/OrderTypes/GetAllOrderTypes");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<decimal?> GetOrderTypePriceByOrderType(int orderTypeId)
        {
            var response = await httpClient.GetAsync("api/OrderTypes/GetOrderTypeItemPriceByOrderType/" + orderTypeId);
            return await ApiStatusCodeHandler.HandleResponse<decimal>(response);
        }

        public async Task<bool> Add(OrderTypeViewModel orderTypesViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/OrderTypes/Add", orderTypesViewModel);
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

        public async Task<bool> Update(OrderTypeViewModel orderTypesViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/OrderTypes/Update", orderTypesViewModel);
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
