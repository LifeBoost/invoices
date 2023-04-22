using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Contractors.DeleteContractor;

public class DeleteContractorCommand : CommandBase
{
    public Guid ContractorId { get; }
    
    public UserId UserId { get; }

    public DeleteContractorCommand(Guid contractorId, UserId userId)
    {
        ContractorId = contractorId;
        UserId = userId;
    }
}