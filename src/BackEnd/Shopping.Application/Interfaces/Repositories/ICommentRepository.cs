using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetById(Guid id, CancellationToken cancellationToken = default);
    void Create(Comment comment);
    void Delete(Comment comment);
}