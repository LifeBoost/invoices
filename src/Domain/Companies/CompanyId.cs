using Domain.SeedWork;

namespace Domain.Companies;

public class CompanyId : TypedIdValueBase
{
    public CompanyId(Guid value) : base(value)
    {
    }
}