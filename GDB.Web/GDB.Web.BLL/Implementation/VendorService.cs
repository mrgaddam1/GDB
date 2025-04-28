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
    }    
}
