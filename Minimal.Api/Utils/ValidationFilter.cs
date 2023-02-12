using Microsoft.Extensions.Logging;
using System.Net;
using FluentValidation;

namespace Minimal.Api.Utils;

public class ValidationFilter : IEndpointFilter
{
    public virtual async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        object? result;

        try
        {
            result = await next(context);
        }
        catch(ValidationException ex)
        {
            return TypedResults.BadRequest(ex.Errors);
        }

        return result;
    }
}

