using Blazored.SessionStorage;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
 
namespace GDB.Web.Client.Security
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly ISessionStorageService SessionStorageService;

        public TokenHandler(ISessionStorageService _SessionStorageService)
        {
            SessionStorageService = _SessionStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await SessionStorageService.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

    }
}
