using System.Net;
using System.Net.Http.Json;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Services;

internal sealed class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.HttpClient);
    }

    public async Task<ResultValue<AuthUserResponse>> LoginByRefreshTokenAsync(LoginByRefreshTokenRequest request, CancellationToken cancellation)
    {
        var result = await _httpClient.PostAsJsonAsync("/Users/Register",request,cancellation);
        return result.StatusCode == HttpStatusCode.InternalServerError ? new Error("Internal server error") : await result.Content.ReadFromJsonAsync<ResultValue<AuthUserResponse>>(cancellation);
    }

    public async Task<ResultValue<AuthUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _httpClient.PostAsJsonAsync("/Users/Login",request,cancellationToken);
        return result.StatusCode == HttpStatusCode.InternalServerError ? new Error("Internal server error") : await result.Content.ReadFromJsonAsync<ResultValue<AuthUserResponse>>(cancellationToken);
    }

    public async Task<ResultValue<AuthUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _httpClient.PostAsJsonAsync("/Users/Login",request,cancellationToken);
        return result.StatusCode == HttpStatusCode.InternalServerError ? new Error("Internal server error") : await result.Content.ReadFromJsonAsync<ResultValue<AuthUserResponse>>(cancellationToken);
    }
}