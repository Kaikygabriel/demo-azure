using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Configurations;

namespace Shopping.Infra.Repositories.Cache;

internal sealed class CacheProductRepository: IProductRepository
{
    private readonly IProductRepository _productRepository;

    public CacheProductRepository([FromKeyedServices(NameConfiguration.RepositoryProduct)]IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Create(Product product)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product product)
    {
        throw new NotImplementedException();
    }
}