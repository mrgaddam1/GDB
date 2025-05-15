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
    public class VendorService : IVendorService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public VendorService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<VendorViewModel>> GetAll<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Vendor/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<VendorViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        public async Task<bool?> Add(VendorViewModel vendorViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Vendor/Add", vendorViewModel);
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

        public async Task<bool?> Update(VendorViewModel vendorViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Vendor/Update", vendorViewModel);
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
