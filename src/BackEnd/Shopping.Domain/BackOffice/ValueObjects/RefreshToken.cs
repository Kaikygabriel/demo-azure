using Shopping.Shared.ValueObjects;

namespace Shopping.Domain.BackOffice.ValueObjects;

public record RefreshToken : ValueObject
{
    public string? Code { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public RefreshToken(string? code, DateTime? expiredAt)
    {
        Code = code;
        ExpiredAt = expiredAt;
    }

    public bool IsValid(string code)
    {
        if (ExpiredAt < DateTime.UtcNow || !code.Equals(code))
            return false;

        return true;
    }
};