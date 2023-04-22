using Application.Configuration.Queries;
using Domain.Users;

namespace Application.BankAccounts.GetAllBankAccountsByCompanyId;

public class GetAllBankAccountsByCompanyIdQuery : IQuery<List<BankAccountDto>>
{
    public Guid CompanyId { get; }
    public UserId UserId { get; }

    public GetAllBankAccountsByCompanyIdQuery(Guid companyId, UserId userId)
    {
        CompanyId = companyId;
        UserId = userId;
    }
}
