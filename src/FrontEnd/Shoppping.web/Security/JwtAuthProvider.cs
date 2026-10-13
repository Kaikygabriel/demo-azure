using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Application.Dto.Users;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;
using Shoppping.web.Services;

namespace Shoppping.web.Security;

public class JwtAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;
    private readonly HttpClient _httpClient;
    private readonly LocalStorage _localStorage;
    
    public JwtAuthProvider(IHttpClientFactory httpClientFactory, LocalStorage localStorage, IAuthService authService)
    {
        _localStorage = localStorage;
        _authService = authService;
        _httpClient = httpClientFactory.CreateClient(Configuration.httpClientAuth);
    }

    private async Task<UserClaimsDto?> GetUser()
    {
        var authUser = _localStorage.Get();
        if(authUser is null)
            return null;

        if (authUser.ExpiredToken >= DateTime.UtcNow )
        {
            if (authUser.ExpiredRefreshToken >= DateTime.UtcNow)
                return null;

            var resultLoginByRefreshToken = await LoginByRefreshToken(authUser);
            if (resultLoginByRefreshToken is null)
                return null;

            authUser = resultLoginByRefreshToken;
        }
        
        var resultRequest = await _httpClient.GetAsync("/Users/Claims");
        if (!resultRequest.IsSuccessStatusCode)
            return null;
        
        return await resultRequest.Content.ReadFromJsonAsync<UserClaimsDto?>();
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {    var result = await GetUser();

        if (result is null)
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, result.Email),
            new(ClaimTypes.Role, result.Role?.FirstOrDefault() ?? "")
        };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);

    }

    private async Task<AuthUserResponse?> LoginByRefreshToken(AuthUserResponse expiredAuthModel)
    {
        var request = new LoginByRefreshTokenRequest(expiredAuthModel.RefreshToken, expiredAuthModel.Id);
        var result = await _authService.LoginByRefreshTokenAsync(request,CancellationToken.None);
        if (result.IsSuccess)
            return null;
        return result.Value;
    }
    
}