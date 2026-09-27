using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class User : Entity
{
    private User()
    {
        
    }
    public User(Email email, Password password,Localization localization,string name)
    {
        Name = name;
        Email = email;
        Password = password;
        Localization = localization;
    }

    public string Name { get;private set; }
    public Email Email { get;private set; }
    public Password Password { get;private set; }
    public Localization Localization { get;private set; }

    public RefreshToken? RefreshToken { get;private set; }
    
    public List<Role> Roles { get;private set; } = [];
    public List<Voucher> Vouchers { get;private set; } = [];

    public void SetRefreshToken(RefreshToken token)
        => RefreshToken = token;
    
    public void SetPassword(Password password)
        => Password = password;

    public Result AddRole(Role role)
    {
        if (Roles.Contains(role))
            return new Error("User contains role !");
        
        Roles.Add(role);
        return Result.Success(); 
    }
    
    public Result RemoveRole(Role role)
    {
        if (!Roles.Contains(role))
            return new Error("User no contains role !");
        
        Roles.Remove(role);
        return Result.Success(); 
    }
}