using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Commum;

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
        }

    public decimal Price { get; private set; }
    public string Title { get; private set; } = null!;
    public string? ImageThumbUrl { get;private  set; }
    public string Summary { get;private  set; }= null!;
    public List<string> ImagesUrl { get;private  set; } = [];
    public DateTime CreateAt  { get; private  init; } = DateTime.UtcNow;
    public int Stock { get;private  set; }
    public decimal Discount { get;private  set; }

    public Category Category { get; private init; }
    public Guid CategoryId { get;private init; }

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
        
        ImagesUrl.Add(newImage);
        
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
    
    public static class Factory
    {
        public static ResultValue<Product> Create(Category category,decimal price, int stock, decimal discount, string title,
            string summary)
        {
            return new Product(category,price, stock, discount, title, summary);
        }
    }
}