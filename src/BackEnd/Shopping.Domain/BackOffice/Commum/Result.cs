namespace Shopping.Domain.BackOffice.Commum;

public sealed class Result
{
    private Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }
    
    private Result()
    {
        IsSuccess = true;
    }
    
    public Error Error { get; private init; }
    public bool IsSuccess { get; private init; }

    public static implicit operator Result(Error error)
        => new (error);

    public static Result Success() => new ();
    public static Result Failure(Error error) => new (error);

}