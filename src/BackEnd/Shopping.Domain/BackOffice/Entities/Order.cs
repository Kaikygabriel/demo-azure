using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Enum;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class Order : Entity
{
    private Order()
    {
        
    }
    public Order(User user,Product product,Voucher? voucher)
    {
        Product = product;
        ProductId = product.Id;

        Voucher = voucher;
        VoucherId = voucher?.Id;

        User = user;
        UserId = user.Id;
        
        StatePayment = EStatePayment.Waiting;
        CreateAt = DateTime.UtcNow;
    }

    public DateTime CreateAt { get; private init; }
    public string? Uri { get; private set; }
    public EStatePayment StatePayment { get; private set; }
    
    public Guid ProductId { get; private init; }
    public Product Product { get; private init; } = null!;

    public Guid UserId { get; private init; }
    public User User { get; private init; }= null!;

    public Voucher? Voucher { get; private init; }
    public Guid? VoucherId { get; private init; }

    public decimal Total => Product.Price - (Voucher?.Value ?? 0);

    public void AlterState(EStatePayment statePayment)
    {
        StatePayment = statePayment;
    }

    public void AlterUri(string newUri)
        => Uri = newUri;
}