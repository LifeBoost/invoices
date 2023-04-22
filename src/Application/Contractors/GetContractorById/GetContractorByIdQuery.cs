using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Contractors.GetContractorById;

public class GetContractorByIdQuery : IQuery<ContractorDTO>
{
    public Guid ContractorId { get; }

    public UserId UserId { get; }

    public GetContractorByIdQuery(Guid contractorId, UserId userId)
    {
        ContractorId = contractorId;
        UserId = userId;
    }
}