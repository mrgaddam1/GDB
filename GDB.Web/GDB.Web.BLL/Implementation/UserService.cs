using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class UserService : IUserService
    {
        public HttpClient httpClient { get; set; }
        public UserService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<T?> UserLogin<T>(LoginRequest loginRequest)
        {
            
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsJsonAsync("api/User/Login", loginRequest);
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
            
        }
      
    }
}
