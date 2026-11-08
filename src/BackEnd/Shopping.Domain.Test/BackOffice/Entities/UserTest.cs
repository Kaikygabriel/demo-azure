using Shopping.Domain.BackOffice.Entities;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Domain.Test.BackOffice.Entities;

public class UserTest
{
    private readonly User _user;

    public UserTest()
    {
        _user = new User(Email.Factory.Create("teste@gmail.com").Value, Password.Factory.Create("teste").Value, new Localization("São Paulo", 53441301),"name");
    }

    [Fact]
    public void Add_Role_Should_Return_Success_When_Is_Valid()
    {
        var role = new Role("admin");

        var result = _user.AddRole(role);

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Add_Role_Should_Return_Error_When_Already_Exists_Role()
    {
        var role = new Role("admin");

        _user.AddRole(role);
        var result = _user.AddRole(role);

        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Alter_Refresh_Token_Should_return_New_Refresh_Token()
    {
        var refreshToken = new RefreshToken("teste", DateTime.UtcNow.AddDays(1));
        _user.SetRefreshToken(refreshToken);
        Assert.Equal(refreshToken.Code,_user.RefreshToken.Code);
    }
    
    [Fact]
    public void Alter_Password_Should_return_New_Password()
    {
        var password = Password.Factory.Create("Teljasfdlkjslfkds2");
        _user.SetPassword(password.Value);
        Assert.Equal(password.Value.Hash,_user.Password.Hash);
    }
}