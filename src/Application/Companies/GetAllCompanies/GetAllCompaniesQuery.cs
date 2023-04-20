using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Companies.GetAllCompanies;

public class GetAllCompaniesQuery : IQuery<List<CompanyDTO>>
{
    public UserId UserId { get; }

    public GetAllCompaniesQuery(UserId userId)
    {
        UserId = userId;
    }
}