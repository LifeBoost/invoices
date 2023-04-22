using System;
using Domain.Users;

namespace Domain.BankAccounts
{
	public interface IBankAccountRepository
	{
		Task<BankAccount> GetByIdAsync(BankAccountId id, UserId userId);

		Task AddAsync(BankAccount bankAccount);

		Task DeleteAsync(BankAccountId id, UserId userId);

		Task SaveAsync(BankAccount bankAccount);
	}
}

