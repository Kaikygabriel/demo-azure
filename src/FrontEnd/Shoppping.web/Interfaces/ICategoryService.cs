using Shopping.Application.Commum;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shoppping.web.Interfaces;

public interface ICategoryService
{
    Task<ResultValue<PagedResponse<List<Category>>>> GetAllAsync();
}