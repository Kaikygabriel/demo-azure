using Shopping.Shared.ValueObjects;

namespace Shopping.Domain.BackOffice.ValueObjects;

public record Localization : ValueObject
{
    public Localization()
    {
        
    }
    public Localization(string address,int cep)
    {
        Cep = cep;
        Address = address;
    }

    public int Cep { get; init; }
    public string Address { get; init; }
}