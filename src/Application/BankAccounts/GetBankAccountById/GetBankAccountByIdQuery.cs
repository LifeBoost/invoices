using Application.Configuration.Queries;
using Domain.Users;

namespace Application.BankAccounts.GetBankAccountById;

public class GetBankAccountByIdQuery : IQuery<BankAccountDto>
{
    public Guid Id { get; }
    public UserId UserId { get; }

    public GetBankAccountByIdQuery(Guid id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}