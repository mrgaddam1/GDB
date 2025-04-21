using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace GDB.Web.Client.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
         
        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // If token is valid, set user as authenticated
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "username@example.com")
            }, "jwt");

            _currentUser = new ClaimsPrincipal(identity);
            return new AuthenticationState(_currentUser);
        }

        public async Task MarkUserAsAuthenticated(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, username)
        }, "jwt");

            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        //public async Task MarkUserAsAuthenticated(string token)
        //{
          
        //    var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        //    var user = new ClaimsPrincipal(identity);
            
           
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        //}

        //public async Task Logout()
        //{
        //    await _sessionStorageService.RemoveItemAsync("authToken");
        //    _user = new ClaimsPrincipal(new ClaimsIdentity());
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        //}

        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims;
        }
    }
}
