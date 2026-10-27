using System.Net;
using System.Net.Http.Json;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shoppping.web.Configurations;

namespace Shoppping.web.Services;

internal sealed class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.httpClientAuth);
    }

    public async Task<ResultValue<Order>> CreatedAsync(CreateOrderRequest request,
        CancellationToken cancellation = default)
    {
        var result = await _httpClient.PostAsJsonAsync("/Orders", request,cancellation);
        if (result.StatusCode == HttpStatusCode.InternalServerError)
            return new Error("Internal server error");

        return await result.Content.ReadFromJsonAsync<ResultValue<Order>>();
    }
}