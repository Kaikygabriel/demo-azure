using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class Comment :Entity
{
    private Comment()
    {
        
    }
    private Comment(byte stars,  Product product, User user, string message)
    {
        Stars = stars;
        ProductId = product.Id;
        Product = product;
        User = user;
        UserId = user.Id;
        Message = message;
    }

    public byte Stars { get; private init; }
    public Guid ProductId { get; private init; }
    public Product Product { get; private init; }
    public User User { get; private init; }
    public Guid UserId { get; private init; }
    public string Message { get; private init; }
    public DateTime CreateAt { get; private init; } = DateTime.UtcNow;
    
    public static class Factory
    {
        public static ResultValue<Comment> Create(byte stars, Product product, User user, string message)
        {
            if (stars > 5)
                return new Error("A star cannot be larger than five");
            if (string.IsNullOrEmpty(message))
                return new Error("Message invalid");

            return new Comment(stars, product, user, message);
        }
    }
}