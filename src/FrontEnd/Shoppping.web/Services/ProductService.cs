using System.Net;
using System.Net.Http.Json;
using Shopping.Application.Commum;
using Shopping.Application.Dto.Products;
using Shopping.Application.UsesCases.Products.Queries.Request;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    
    public ProductService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(Configuration.httpClientAuth);
    }

    public async Task<ResultValue<PagedResponse<List<ProductDto>>>> GetAllAsync(int page, int pageSize)
    {
        var request = new GetAllProductsRequest()
        {
            Page = page,
            PageSize = pageSize
        };
        var result = await _httpClient.GetAsync($"/Products?Page={request.Page}&PageSize={request.PageSize}");
        return result.StatusCode == HttpStatusCode.InternalServerError
            ? new Error("Internal server error!")
            : await result.Content.ReadFromJsonAsync<ResultValue<PagedResponse<List<ProductDto>>>>();
    }
}