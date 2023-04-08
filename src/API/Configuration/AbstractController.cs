using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Configuration;

public abstract class AbstractController : Controller
{
    protected string GetAuthorizationToken()
    {
        var authorizationHeaders = Request.Headers.Authorization;

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