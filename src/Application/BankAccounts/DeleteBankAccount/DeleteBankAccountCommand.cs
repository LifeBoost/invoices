using System;
using Application.Configuration.Commands;
using Domain.Users;

namespace Application.BankAccounts.DeleteBankAccount
{
	public class DeleteBankAccountCommand : CommandBase
	{
		public Guid Id { get; }
		public UserId UserId { get; }

		public DeleteBankAccountCommand(Guid id, UserId userId)
        {
			Id = id;
			UserId = userId;
		}
	}
}

