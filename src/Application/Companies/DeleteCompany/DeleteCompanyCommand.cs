using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Companies.DeleteCompany;

public class DeleteCompanyCommand : CommandBase
{
    public Guid Id { get; }
    public UserId UserId { get; }
    
    public DeleteCompanyCommand(Guid id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}