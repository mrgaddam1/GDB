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
    public class StaterService(HttpClient _httpClient) : IStaterService
    {
        public HttpClient httpClient { get; set; } = _httpClient;
   
        public async Task<T?> GetAllStaters<T>()
        {
            var response = await httpClient.GetAsync("api/Staters/GetAllStaters");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<decimal> GetStaterPriceByStater(int staterId)
        {
            var response = await httpClient.GetAsync("api/Staters/GetStaterPriceByStater/" + staterId);
            return await ApiStatusCodeHandler.HandleResponse<decimal>(response);
        }

        public async Task<bool> Add(StatersViewModel statersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Staters/Add", statersViewModel);
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


        public async Task<bool> Update(StatersViewModel statersViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Staters/Update", statersViewModel);
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
