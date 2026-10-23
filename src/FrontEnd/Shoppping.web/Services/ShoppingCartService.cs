using System.Net;
using System.Net.Http.Json;
using Shopping.Application.Dto.ShoppingCart;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Services;

public sealed class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.httpClientAuth);
    }

    public async Task<ResultValue<ShoppingCartDto>> GetByUserId(Guid userId)
    {
        var result = await _httpClient.GetAsync($"/ShoppingCart/v1?UserId={userId}");
        if (result.StatusCode is HttpStatusCode.InternalServerError)
            return new Error("Internal server error!");

        return await result.Content.ReadFromJsonAsync<ResultValue<ShoppingCartDto>>();
    }
}