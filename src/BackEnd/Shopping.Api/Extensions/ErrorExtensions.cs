using Microsoft.AspNetCore.Mvc;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Api.Extensions;

public static class ErrorExtensions
{
    public static ProblemDetails ToProblemDetails(this Error error)
    {
        // if(error.Message.Contains("not found",StringComparison.InvariantCultureIgnoreCase))
        //     return new ProblemDetails()
        //     {
        //         Title = error.Message,
        //         Detail = error.Message,
        //         Status = StatusCodes.Status404NotFound,
        //         Type = "error"
        //     };
        
        return new ProblemDetails()
        {
            Title = error.Message,
            Detail = error.Message,
            Status = StatusCodes.Status400BadRequest,
            Type = "error"
        };
    }
}