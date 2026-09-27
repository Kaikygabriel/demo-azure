namespace Shopping.Domain.BackOffice.Commum;

public sealed class ResultValue<T>
{
    public ResultValue()
    {
        
    }
    private ResultValue(Error error)
    {
        Error = error;
        IsSuccess = false;
    }
    
    private ResultValue(T value)
    {
        Value = value;
        IsSuccess = true;
    }
    
    public Error Error { get; private init; }
    public bool IsSuccess { get; private init; }
    public T Value { get; private init; }

    public static implicit operator ResultValue<T>(Error error)
        => new (error);
    
    public static implicit operator ResultValue<T>(T value)
        => new (value);

    public static ResultValue<T> Success(T value) => new (value);
    public static ResultValue<T> Failure(Error error) => new (error);

}