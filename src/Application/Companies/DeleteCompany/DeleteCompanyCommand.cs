using Application.Configuration.Commands;
using Domain.Companies;
using Domain.Users;

namespace Application.Companies.DeleteCompany;

public class DeleteCompanyCommand : CommandBase
{
    public CompanyId Id { get; }
    public UserId UserId { get; }

    public DeleteCompanyCommand(CompanyId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}