using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface IVoucherRepository
{
    Task<Voucher?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
}