using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Contractors.GetAllContractors;

public class GetAllContractorsQuery : IQuery<List<ContractorDTO>>
{
    public UserId UserId { get; }

    public GetAllContractorsQuery(UserId userId)
    {
        UserId = userId;
    }
}