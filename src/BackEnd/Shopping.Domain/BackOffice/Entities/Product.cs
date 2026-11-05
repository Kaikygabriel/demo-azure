using System.Collections.ObjectModel;
using Shopping.Shared.Entity;using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class Product : Entity
{
    private Product()
    {
        
    }
    private  Product(Category category,decimal price, int stock, decimal discount,string title,string summary)
    {
        Category = category;
        CategoryId = category.Id;
        Price = price;
        Stock = stock;
        Title = title;
        Summary = summary;
        Stars = 0;
    }

    public Byte Stars { get; set; }
    public decimal Price { get; private set; }
    public string Title { get; private set; } = null!;
    public string? ImageThumbUrl { get;private  set; }
    public string Summary { get;private  set; }= null!;

    private IList<string> _imagesUrl = [];
    
    public IReadOnlyCollection<string> ImagesUrl
    {
        get { return (ReadOnlyCollection<string>)_imagesUrl;  }
    } 
    public DateTime CreateAt  { get; private  init; } = DateTime.UtcNow;
    public int Stock { get;private  set; }
    public decimal Discount { get;private  set; }

    public Category Category { get; private init; }
    public Guid CategoryId { get;private init; }

    private IList<Comment> _comments = [];
    public ReadOnlyCollection<Comment> Comments
    {
        get { return (ReadOnlyCollection<Comment>)_comments; }
    } 

    public Result AddComment(Comment comment)
    {
        if (_comments.Any(x => x.UserId == comment.UserId))
            return new Error("The user has already commented on the product");
        _comments.Add(comment);
        UpdateStars();
        return Result.Success();
    }
    
    public Result RemoveComment(Comment comment)
    {
        if (!Comments.Any(x => x.Id == comment.Id))
            return new Error("Comment not found");
        _comments.Remove(comment);
        UpdateStars();
        return Result.Success();
    }
    
    public decimal GetTotal()
        => Price - Discount;
    
    public Result AlterPrice(decimal newPrice)
    {
        if (newPrice <= 0)
            return new Error("Price is invalid");

        Price = newPrice;
        
        return Result.Success();
    }
    public Result AddImage(string newImage)
    {
        if (ImagesUrl.Contains(newImage))
            return new Error("Image url already exists in images");
        
        _imagesUrl.Add(newImage);
        
        return Result.Success();
    }
    
    public Result AlterStock(int newStock)
    {
        if (newStock < 0)
            return new Error("Stock not found !");

        Stock = newStock;

        return Result.Success();
    }
    
    public Result AlterDiscount(decimal discount)
    {
        if (discount < 0)
            return new Error("Discount Invalid");
        
        Discount = discount;
        
        return Result.Success();
    }
    public void SetImageThumb(string url)
        => ImageThumbUrl = url;

    private void UpdateStars()
    {
        Stars = (byte)Comments.Average(x => x.Stars);
    }
    
    public static class Factory
    {
        public static ResultValue<Product> Create(Category category,decimal price, int stock, decimal discount, string title,
            string summary)
        {
            return new Product(category,price, stock, discount, title, summary);
        }
    }
}