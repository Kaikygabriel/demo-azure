using Shopping.Application.Dto.Products;
using Shopping.Application.Commum;

namespace Shopping.Application.Interfaces.Queries;

public interface IProductQuery
{
    Task<PagedResponse<List<ProductDto>>> GetAllAsync(int page, int pageSize, CancellationToken cancellation = default);
    Task<ProductDetailsDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}