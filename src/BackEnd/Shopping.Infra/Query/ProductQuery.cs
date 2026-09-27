using Microsoft.EntityFrameworkCore;
using Shopping.Application.Commum;
using Shopping.Application.Dto.Products;
using Shopping.Application.Interfaces.Queries;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Query;

internal sealed class ProductQuery : IProductQuery
{
    private readonly AppDbContext _appDbContext;

    public ProductQuery(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<PagedResponse<List<ProductDto>>> GetAllAsync(int page, int pageSize, CancellationToken cancellation = default)
    {
        var products = await _appDbContext.Products.Skip((1 - page) * pageSize).Take(pageSize).Select(x =>
                new ProductDto(x.Id, x.Title, x.Summary, x.ImageThumbUrl, x.Price, x.Discount, x.Category.Title))
            .ToListAsync(cancellation);

        var total = await _appDbContext.Products.CountAsync(cancellation);
        return new PagedResponse<List<ProductDto>>(page, pageSize, products, total);
    }

    public async Task<ProductDetailsDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Products.Select(x=> 
                new ProductDetailsDto(x.Id,x.Category,x.Title,x.Summary,x.ImageThumbUrl,x.ImagesUrl,x.Price,x.Discount,x.Stock))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}