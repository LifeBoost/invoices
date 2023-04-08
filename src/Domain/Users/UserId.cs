using Domain.SeedWork;

namespace Domain.Users;

public class UserId : TypedIdValueBase
{
    public UserId(Guid value) : base(value)
    {
    }
}