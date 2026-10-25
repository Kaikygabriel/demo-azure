using System.Net;
using System.Net.Http.Json;
using Shopping.Application.Commum;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shoppping.web.Configurations;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.httpClientAuth);
    }

    public async Task<ResultValue<PagedResponse<List<Category>>>> GetAllAsync()
    {
        var result = await _httpClient.GetAsync("/Categories?page=1&pageSize=25");
        if (result.StatusCode is HttpStatusCode.InternalServerError)
            return new Error("Internal Server Error");

        return await result.Content.ReadFromJsonAsync<ResultValue<PagedResponse<List<Category>>>>();
    }
}