using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Domain.Test.BackOffice.Entities;

public sealed class ProductTest
{
    private static Category Category_Valid = new Category("teste");
    private readonly Product Product = Product.Factory.Create(Category_Valid, Price, Stock, Discount, Title, Summary).Value;
    
    private const decimal Price = 12;
    private const int Stock = 12;
    private const decimal Discount = 0;
    private const string Title = "teste product";
    private const string Summary = "teste teste";

    [Fact]
    public void Create_Product_Should_Return_True_When_Parameters_Is_Valid()
    {
        var resultCreateProduct = Product.Factory.Create(Category_Valid, Price, Stock, Discount, Title, Summary);
        Assert.True(resultCreateProduct.IsSuccess);
    }

    [Fact]
    public void Alter_Price_In_Product_Should_Return_New_Price()
    {
        var newPrice = 24;
        var result = Product.AlterPrice(newPrice);
        Assert.True(result.IsSuccess && Product.Price == newPrice);
    }
    
    [Fact]
    public void Alter_Discount_In_Product_Should_Return_New_Discount()
    {
        var newDiscount = 2;
        var result = Product.AlterDiscount(newDiscount);
        Assert.True(result.IsSuccess && Product.Discount == newDiscount);
    }
    
    [Fact]
    public void Add_Image_In_Product_Should_Return_New_Image()
    {
        var image = "teste.jpg";
        var result = Product.AddImage(image);
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public void Set_Image_Thumb_In_Product_Should_Return_New_Image()
    {
        var image = "teste.jpg";
        Product.SetImageThumb(image);
        Assert.True(Product.ImageThumbUrl?.Equals(image));
    }
}