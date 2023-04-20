using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Companies.GetCompanyById;

public class GetCompanyByIdQuery : IQuery<CompanyDTO>
{
    public Guid Id { get; }
    public UserId UserId { get; }

    public GetCompanyByIdQuery(Guid id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}