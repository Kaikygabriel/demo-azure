using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.ValueObjects;

public sealed record Email
{
    private Email()
    {
        
    }
    private Email(string address)
    {
        Address = address;
    }

    public string Address { get; private init; }

    public static class Factory
    {
        public static ResultValue<Email> Create(string address)
        {
            if (AddressIsInvalid(address))
                return new Error("Address in email is invalid");
            
            return new Email(address);
        }
    }

    private static bool AddressIsInvalid(string address)
        => string.IsNullOrEmpty(address) ||
           !address.Contains('@') ||
           address.Split('@').Length != 2 ||
           address.Split('@')[0].Length <= 3 ||
           address.Split('@')[1].Length <= 3;

}