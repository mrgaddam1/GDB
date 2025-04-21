using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
