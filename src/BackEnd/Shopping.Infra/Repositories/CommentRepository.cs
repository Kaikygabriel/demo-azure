using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _appDbContext;

    public CommentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Comment?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Create(Comment comment)
    {
        _appDbContext.Comments.Add(comment);
    }

    public void Delete(Comment comment)
    {
        _appDbContext.Comments.Remove(comment);
    }
}