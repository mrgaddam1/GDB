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
    public class GroceryService : IGroceryService
    {
        public HttpClient httpClient { get; set; }
        
        public GroceryService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<GroceryViewModel>> GetAll<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Grocery/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<GroceryViewModel>>(response);
                //return await ApiStatusCodeHandler.HandleResponse<List<GroceryViewModel>>(response);
                return data;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
                throw new ApplicationException();
            }
       
        }


        public async Task<bool?> Add(GroceryViewModel groceryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/grocery/Add", groceryViewModel);
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

        public async Task<bool?> Update(GroceryViewModel groceryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/grocery/Update", groceryViewModel);
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
