using System;
using Application.Configuration.Commands;
using Domain;
using Domain.Users;

namespace Application.BankAccounts.CreateBankAccount
{
	public class CreateBankAccountCommand : CommandBase<BankAccountDto>
	{
		public UserId UserId { get; }
		public Guid CompanyId { get; }
		public string Name { get; }
		public string BankName { get; }
		public string BankAccountNumber { get; }
		public Currency Currency { get; }

		public CreateBankAccountCommand(
			UserId userId,
			Guid companyId,
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
		)
		{
			UserId = userId;
			CompanyId = companyId;
			Name = name;
			BankName = bankName;
			BankAccountNumber = bankAccountNumber;
			Currency = currency;
		}
	}
}
