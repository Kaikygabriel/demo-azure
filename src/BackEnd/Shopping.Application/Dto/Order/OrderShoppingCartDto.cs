using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Dto.Order;

public record OrderShoppingCartDto(Guid Id, ProductShoppingCartDetailsDto Product, Voucher? Voucher)
{
    public decimal Total()
        => Voucher is null || Product.Discount >0 ? Product.Price : Product.Price - Voucher.Value;
    
};