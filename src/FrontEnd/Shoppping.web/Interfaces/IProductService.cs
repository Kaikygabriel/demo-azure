using Shopping.Application.Commum;
using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Commum;

namespace Shoppping.web.Interfaces;

public interface IProductService
{
    Task<ResultValue<PagedResponse<List<ProductDto>>>> GetAllAsync(int page, int pageSize);
}