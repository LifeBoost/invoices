using System.Security.Authentication;
using System.Text.Json;
using Domain.Users;

namespace Infrastructure.Domain.Users;

public class AuthenticationServiceUserContext : IUserContext
{
    private readonly string _authenticationServiceUrl;
    private const string MeEndpoint = "/api/v1/auth/user";

    public AuthenticationServiceUserContext(string authenticationServiceUrl)
    {
        this._authenticationServiceUrl = authenticationServiceUrl;
    }

    public async Task<UserId> GetUserId(string token)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"{this._authenticationServiceUrl}{MeEndpoint}");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = httpClient.GetAsync("").Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new AuthenticationException();
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(responseBody);

        if (user == null)
        {
            throw new AuthenticationException();
        }

        return new UserId(new Guid(user.Id));
    }
}