using Domain.SeedWork;

namespace Domain.Addresses;

public class AddressId : TypedIdValueBase
{
    public AddressId(Guid value) : base(value)
    {
    }
}