using Application.Configuration.Commands;
using Domain;
using Domain.Users;

namespace Application.BankAccounts.UpdateBankAccount
{
	public class UpdateBankAccountCommand : CommandBase
	{
		public Guid Id { get; }
		public UserId UserId { get; }
		public string Name { get; }
		public string BankName { get; }
		public string BankAccountNumber { get; }
		public Currency Currency { get; }

		public UpdateBankAccountCommand(
			Guid id,
			UserId userId,
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
		)
		{
			Id = id;
			UserId = userId;
			Name = name;
			BankName = bankName;
			BankAccountNumber = bankAccountNumber;
			Currency = currency;
		}
	}
}

