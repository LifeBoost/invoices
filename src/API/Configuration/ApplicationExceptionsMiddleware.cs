using System.Security.Authentication;
using Application.Configuration.Validation;
using Domain.Companies.Exceptions;
using Domain.SeedWork;

namespace API.Configuration;

public class ApplicationExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    
    public ApplicationExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (BusinessRuleValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new { Error = exception.Message });
        }
        catch (AuthenticationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        catch (InvalidCommandException exception)
        {
            var response = new
            {
                title = "Validation error",
                status = StatusCodes.Status400BadRequest,
                errors = exception.Errors,
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (CompanyNotFoundException exception)
        {
            var response = new
            {
                error = exception.Message
            };

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}