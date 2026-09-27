using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Dto.Order;

public record OrderDto(Guid Id, Product Product, Voucher? Voucher, Guid UserId, string Email)
{
    public decimal Total()
        => Voucher is null || Product.Discount >0 ? Product.GetTotal() : Product.GetTotal() - Voucher.Value;
};