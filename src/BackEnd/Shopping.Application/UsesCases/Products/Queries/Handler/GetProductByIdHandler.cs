using Shopping.Application.Dto.Products;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.UsesCases.Products.Queries.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Queries.Handler;

internal sealed class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest,ResultValue<ProductDetailsDto>>
{
    private readonly IProductQuery _productQuery;

    public GetProductByIdHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public async Task<ResultValue<ProductDetailsDto>> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _productQuery.GetByIdAsync(request.Id,cancellationToken);
        if (product is null)
            return new Error("Product not found !");
        
        return product;
    }
}