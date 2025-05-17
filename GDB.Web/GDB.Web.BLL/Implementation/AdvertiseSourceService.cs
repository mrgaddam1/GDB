using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class AdvertiseSourceService : IAdvertiseSourceService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public AdvertiseSourceService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<T?> GetAllAdvertiseSourceDetails<T>()
        {
            var response = await httpClient.GetAsync("api/AdvertiseSource/GetAllAdvertiseSources");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
        public async Task<bool?> Add(AdvertiseSourceViewModel advertiseSourceViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/AdvertiseSource/Add", advertiseSourceViewModel);
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

        public async Task<bool?> Update(AdvertiseSourceViewModel advertiseSourceViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/AdvertiseSource/Update", advertiseSourceViewModel);
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
