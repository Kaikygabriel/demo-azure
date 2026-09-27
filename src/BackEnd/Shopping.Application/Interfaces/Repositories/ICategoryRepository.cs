using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll(int page = 0,int pageSize = 25,CancellationToken cancellationToken = default);
    Task<int> GetTotal(CancellationToken cancellationToken = default);
    Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void Create(Category category,CancellationToken cancellationToken = default);
}