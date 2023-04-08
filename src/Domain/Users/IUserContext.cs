namespace Domain.Users;

public interface IUserContext
{
    Task<UserId> GetUserId(string token);
}