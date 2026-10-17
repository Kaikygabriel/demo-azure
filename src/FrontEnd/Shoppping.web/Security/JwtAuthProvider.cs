using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Application.Dto.Users;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
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

    public async Task<bool> IsAuthenticated()
    {
        var result =await GetAuthenticationStateAsync();
        return result.User.Identity?.IsAuthenticated == true;
    }
    private async Task<UserClaimsDto?> GetUser()
    {
        var authUser = _localStorage.Get();
        
        if(authUser is null)
        {
            Console.WriteLine("User null");
            return null;
        }

        if (authUser.ExpiredToken <= DateTime.UtcNow )
        {
            if (authUser.ExpiredRefreshToken <= DateTime.UtcNow)
                return null;

            var resultLoginByRefreshToken = await LoginByRefreshToken(authUser);
            if (resultLoginByRefreshToken is null)
                return null;

            authUser = resultLoginByRefreshToken;
            _localStorage.Insert(authUser);
        }
        
        var resultRequest = await _httpClient.GetAsync("/Users/ClaimsPrincipal");
        if (!resultRequest.IsSuccessStatusCode)
            return null;
        var result = await resultRequest.Content.ReadFromJsonAsync<ResultValue<UserClaimsDto>>();
        if (!result.IsSuccess)
            return null;
        
        return result.Value;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {    
        var result = await GetUser();
        if (result is null)
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));
        
        var claims = new List<Claim>()
        { 
             new(ClaimTypes.Name, result.Email)
        };
        
        foreach (var role in result.Role)
            claims.Add(new (ClaimTypes.Role,role));
        
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
    public async Task UpdateAuthenticationStateAsync()
    {
        try
        {
            Console.WriteLine("Executou");
            NotifyAuthenticationStateChanged(
                Task.FromResult(await GetAuthenticationStateAsync()));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
         
    }
}