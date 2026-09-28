using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _appDbContext;

    public VoucherRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Voucher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Create(Voucher voucher)
    {
        _appDbContext.Vouchers.Add(voucher);
    }

    public void Update(Voucher voucher)
    {
        _appDbContext.Vouchers.Update(voucher);
    }

    public void Delete(Voucher voucher)
    {
        _appDbContext.Vouchers.Remove(voucher);
    }
}