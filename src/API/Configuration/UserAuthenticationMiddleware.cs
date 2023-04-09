using System.Security.Authentication;
using Domain.Users;

namespace API.Configuration;

public class UserAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    
    public UserAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserContext userContext)
    {
        var token = GetAuthorizationToken(context);

        context.Items.Add("UserId", await userContext.GetUserId(token));

        await _next.Invoke(context);
    }

    private static string GetAuthorizationToken(HttpContext context)
    {
        var authorizationHeaders = context.Request.Headers.Authorization;

        if (authorizationHeaders.Count == 0)
        {
            throw new AuthenticationException();
        }

        var authorizationHeader = authorizationHeaders[0];

        if (authorizationHeader == null)
        {
            throw new AuthenticationException();
        }

        return authorizationHeader.Replace("Bearer ", "");
    }
}