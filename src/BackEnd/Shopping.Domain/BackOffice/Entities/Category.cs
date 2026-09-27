using Shopping.Domain.BackOffice.Abstraction;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class Category : Entity
{
    private Category()
    {
        
    }
    public Category(string title)
    {
        Title = title;
    }

    public string Title { get; private set; } = null!;
    public List<Product> Products { get; set; } = [];
}