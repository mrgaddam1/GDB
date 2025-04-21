using GDB.Web.BLL.Interface;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class TwilioService : ITwilioService
    {
        private readonly HttpClient _httpClient;

        public TwilioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessage(MessagesViewModel messages)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TwilioSMS/SendSms", messages);

            return response.IsSuccessStatusCode;
        }
    }
 }
