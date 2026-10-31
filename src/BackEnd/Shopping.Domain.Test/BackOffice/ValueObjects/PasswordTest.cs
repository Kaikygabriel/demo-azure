using System.ComponentModel;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Domain.Test.BackOffice.ValueObjects;

[Category("Password")]
public class PasswordTest
{
    private const string PASSWORD_VALID = "1234teste";
    private const string PASSWORD_INVALID_SMALL = "123";
    private const string PASSWORD_INVALID_NULL = null;

    [Fact]
    public void Create_Password_Should_Return_True_When_Valid()
    {
        var result = Password.Factory.Create(PASSWORD_VALID);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Password_Should_Return_False_When_Password_Small()
    {
        var result = Password.Factory.Create(PASSWORD_INVALID_SMALL);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Password_Should_Return_False_When_Null()
    {
        var result = Password.Factory.Create(PASSWORD_INVALID_NULL);
        Assert.False(result.IsSuccess);
    }
    
}