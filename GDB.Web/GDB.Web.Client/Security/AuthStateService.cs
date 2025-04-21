using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace GDB.Web.Client.Security
{
    public class AuthStateService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        public event Action? OnAuthStateChanged;

        public AuthStateService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }

        public async Task<List<string>> GetUserRoles()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            return authState.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }

        public void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }
    }
}
