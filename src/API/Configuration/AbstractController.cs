using System.Security.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Configuration;

public abstract class AbstractController : Controller
{
    protected UserId GetUserId()
    {
        HttpContext.Items.TryGetValue("UserId", out var userId);

        if (userId == null)
        {
            throw new AuthenticationException();
        }

        return (UserId)userId;
    }
}