using System.Text.RegularExpressions;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Shared.ValueObjects;

namespace Shopping.Domain.BackOffice.ValueObjects;

public record Email : ValueObject
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
           !Regex.Match(address,@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;

}