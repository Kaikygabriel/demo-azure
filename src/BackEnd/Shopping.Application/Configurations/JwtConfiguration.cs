namespace Shopping.Application.Configurations;

public class JwtConfiguration
{
    public JwtConfiguration(string key)
    {
        Key = key;
    }
    public string Key { get; private set; }
    public static int ExpiredTokenInHours { get; } = 8;
    public static int ExpiredRefreshTokenInHours { get;} = 16;
}