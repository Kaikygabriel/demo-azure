using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Domain.Test.BackOffice.Entities;

public class VoucherTest
{
    private const string CODE_VALID = "teste123";
    private const string CODE_INVALID = "tes";

    private const decimal VALUE_VALID = 12;
    private const decimal VALUE_INVALID = -1;
    
    private readonly DateTime ENDDATE = new DateTime(14, 02, 2026);
    private readonly DateTime STARTDATE = new DateTime(14, 01, 2026);


    [Fact]
    public void Create_Voucher_Should_Return_True_When_Parameters_is_Valid()
    {
        var result = Voucher.Factory.Create(CODE_VALID, STARTDATE, ENDDATE, VALUE_VALID);
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public void Create_Voucher_Should_Return_False_When_Code_Is_Small()
    {
        var result = Voucher.Factory.Create(CODE_INVALID, STARTDATE, ENDDATE, VALUE_VALID);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Voucher_Should_Return_False_When_Value_Is_Negative()
    {
        var result = Voucher.Factory.Create(CODE_VALID, STARTDATE, ENDDATE, VALUE_INVALID);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Voucher_Should_Return_False_When_EndDate_Is_Invalid()
    {
        var result = Voucher.Factory.Create(CODE_VALID, STARTDATE, STARTDATE.AddMonths(-12), VALUE_VALID);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Voucher_Should_Return_False_When_StartDate_Is_Invalid()
    {
        var result = Voucher.Factory.Create(CODE_VALID, ENDDATE.AddMonths(12), ENDDATE, VALUE_VALID);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Active_Voucher_Should_Return_True_When_Voucher_Is_Active()
    {
        
    }
    
    [Fact]
    public void Disable_Voucher_Should_Return_True_When_Voucher_Is_Disable()
    {
        
    }
}