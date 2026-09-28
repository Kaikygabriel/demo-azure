namespace Shopping.Domain.BackOffice.ValueObjects;

public record RefreshToken(string? Code, DateTime? ExpiredAt)
{
    public bool IsValid(string code)
    {
        if (ExpiredAt < DateTime.UtcNow || !code.Equals(code))
            return false;

        return true;
    }
};