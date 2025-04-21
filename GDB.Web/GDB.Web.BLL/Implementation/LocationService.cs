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
    public class LocationService : ILocationService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public LocationService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<LocationViewModel>> GetAllLocations<T>()
        {
           try
            {
                var response = await httpClient.GetAsync("api/Locations/GetAllLocations");
                var data = await ApiStatusCodeHandler.HandleResponse<List<LocationViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new ApplicationException();
            }

        }

        public async Task<bool?> Add(LocationViewModel locationViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Locations/Add", locationViewModel);
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

        public async Task<bool?> Update(LocationViewModel locationViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Locations/Update", locationViewModel);
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
