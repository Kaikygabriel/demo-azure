using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AnyAsync(x => x.Title == title, cancellationToken);
    }

    public void Create(Product product)
    {
        _dbContext.Products.Add(product);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);

    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }
}