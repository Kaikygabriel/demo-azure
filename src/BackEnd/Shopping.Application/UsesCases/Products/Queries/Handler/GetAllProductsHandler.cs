using Shopping.Application.Commum;
using Shopping.Application.Dto.Products;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.UsesCases.Products.Queries.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Queries.Handler;

internal sealed class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest,ResultValue<PagedResponse<List<ProductDto>>>>
{
    private readonly IProductQuery _productQuery;

    public GetAllProductsHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public async Task<ResultValue<PagedResponse<List<ProductDto>>>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken )
        => await _productQuery.GetAllAsync(request.Page,request.PageSize,cancellationToken);
    
}