using Shopping.Domain.BackOffice.Abstraction;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Domain.BackOffice.Entities;

public sealed class Voucher : Entity
{ 
    private Voucher()
    {
        
    }
    private Voucher(string code,DateTime startDate,DateTime endDate,decimal value)
    {
        Code = code;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = false;
        Value = value;
    }

    public DateTime StartDate { get;private set; }
    public DateTime EndDate { get;private set; }
    public string Code { get;private init; }
    public decimal Value { get;private set; }
    public bool IsActive { get;private set; }

    public Result Active()
    {
        if (IsActive)
            return new Error("Already active");
        IsActive = true;
        return Result.Success();
    }
    
    public Result Disable()
    {
        if (!IsActive)
            return new Error("Already disable");
        IsActive = false;
        return Result.Success();
    }

    public Result AlterStartDate(DateTime newDate)
    {
        if (EndDate < newDate)
            return new Error("Invalid");
        
        StartDate = newDate;
        
        return Result.Success();
    }
    
    public Result AlterEndDate(DateTime newEnd)
    {
        if (StartDate > newEnd)
            return new Error("Invalid");
        
        EndDate = newEnd;
        
        return Result.Success();
    }
    
    public static class Factory
    {
        public static ResultValue<Voucher> Create(string code, DateTime startDate, DateTime endDate, decimal value)
        {
            if(IsInvalid(code,startDate,endDate,value))
                return new Error("Parameters invalid !");
            
            return new Voucher(code,startDate, endDate,  value);
        }
    }

    private static bool IsInvalid(string code, DateTime startDate, DateTime endDate, decimal value)
    {
        if(string.IsNullOrEmpty(code) || code.Length <=3)
            return true;
        if(startDate >= endDate )
            return true;
        
        if (endDate < DateTime.UtcNow)
            return true;

        if (value <= 0)
            return true;
        
        return false;
    }
}