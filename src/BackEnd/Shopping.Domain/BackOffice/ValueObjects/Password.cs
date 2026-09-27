using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.ValueObjects;

public sealed record Password
{
    private Password()
    {
        
    }

    private Password(string password)
    {
        Hash = CreateHash(password);
    }
    
    public string Hash { get; private init; }

    private string CreateHash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string otherPassword)
        => BCrypt.Net.BCrypt.Verify(otherPassword,Hash);

    public static class Factory
    {
        public static ResultValue<Password> Create(string password)
        {
            if (PasswordIsInvalid(password))
                return new Error("Password is invalid");
            
            return new Password(password);
        }
    }

    private static bool PasswordIsInvalid(string password)
        => string.IsNullOrEmpty(password) || password.Length <= 3;
}