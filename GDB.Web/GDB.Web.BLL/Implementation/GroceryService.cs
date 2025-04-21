using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
      
    }
}
