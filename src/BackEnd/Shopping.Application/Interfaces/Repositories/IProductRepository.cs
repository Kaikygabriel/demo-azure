using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken = default);
    
    void Create(Product product);
    void Update(Product product);
    void Delete(Product product);
}