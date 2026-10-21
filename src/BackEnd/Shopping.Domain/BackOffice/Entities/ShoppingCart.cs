using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.Entities;

public class ShoppingCart : Entity
{
    private ShoppingCart()
    {
        
    }
    public ShoppingCart(User user)
    {
        UserId = user.Id;
        User = user;
    }

    public List<Order> Orders { get; private set; } = [];
    public Guid UserId { get; private init; }
    public User User { get; private init; }
    public decimal Total { get;private set; }
    public string UriPayment { get;private set; }

    public void AlterUri(string newUri)
        => UriPayment = newUri;
    public Result AddOrder(Order order)
    {
        if (Orders.Exists(x => x.Id == order.Id) || order.UserId != UserId)
            return new Error("Order invalid");
        
        Orders.Add(order);
        UpdateTotal();
        
        return Result.Success();
    }
    public Result Remove(Order order)
    {
        if (!Orders.Exists(x => x.Id == order.Id) || order.UserId != UserId)
            return new Error("Order not found");
        
        Orders.Remove(order);
        UpdateTotal();
        
        return Result.Success();
    }

    private void UpdateTotal()
        => Total = Orders.Sum(x => x.Total);
}