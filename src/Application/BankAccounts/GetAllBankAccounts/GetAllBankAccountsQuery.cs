using System;
using Application.Configuration.Queries;
using Domain.Users;

namespace Application.BankAccounts.GetAllBankAccounts
{
	public class GetAllBankAccountsQuery : IQuery<List<BankAccountDto>>
	{
		public UserId UserId { get; }

		public GetAllBankAccountsQuery(UserId userId)
		{
			UserId = userId;
		}
	}
}

