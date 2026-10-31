using System.ComponentModel;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Domain.Test.BackOffice.ValueObjects;


[Category("Email")]
public class EmailTest
{
    private const string ADDRESS_INVALID_SMALL = "kai@gm";
    private const string ADDRESS_INVALID_NO_CONTAINS_ARROBA = "kaikygmail.com";
    private const string ADDRESS_VALID = "kaiky@gmail.com";
    
    [Fact]
    public void Create_Email_Should_Return_True_If_Address_is_Valid()
    {
        var resultCreateEmail = Email.Factory.Create(ADDRESS_VALID);
        Assert.True(resultCreateEmail.IsSuccess);
    }
    
    [Fact]
    public void Create_Email_Should_Return_False_If_Address_No_Contains_Arroba()
    {
        var resultCreateEmail = Email.Factory.Create(ADDRESS_INVALID_NO_CONTAINS_ARROBA);
        Assert.False(resultCreateEmail.IsSuccess);
    }
    [Fact]
    public void Create_Email_Should_Return_False_If_Address_Small()
    {
        var resultCreateEmail = Email.Factory.Create(ADDRESS_INVALID_SMALL);
        Assert.False(resultCreateEmail.IsSuccess);
    }
}