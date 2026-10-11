using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Domain.BackOffice.Entities;
using Shoppping.web.Configurations;
using Shoppping.web.Services;

namespace Shoppping.web.Security;

public class JwtAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly LocalStorage _localStorage;
    
    public JwtAuthProvider(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
    {
        _localStorage = localStorage;
        _httpClient = httpClientFactory.CreateClient(Configuration.HttpClient);
    }

    private async Task<User?> GetUser()
    {
        var authUser = _localStorage.Get();
        if()
        
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        
    }
}