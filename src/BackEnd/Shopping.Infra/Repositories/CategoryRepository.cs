using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public CategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Category>> GetAll(int page = 0, int pageSize = 25, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Categories.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotal(CancellationToken cancellationToken = default)
    {
         return await _appDbContext.Categories.CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken = default)
    {
         return await _appDbContext.Categories.AnyAsync(x=>x.Title == title,cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Categories.FirstOrDefaultAsync(x=>x.Id == id,cancellationToken);
    }

    public void Create(Category category, CancellationToken cancellationToken = default)
    {
        _appDbContext.Categories.Add(category);
    }
}