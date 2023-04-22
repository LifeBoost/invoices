using Domain.Companies;
using Domain.Users;

namespace Domain.BankAccounts
{
	public class BankAccount
	{
		public BankAccountId Id { get; private set; }
		public UserId UserId { get; private set; }
		public CompanyId CompanyId { get; private set; }
		public string Name { get; private set; }
		public string BankName { get; private set; }
		public string BankAccountNumber { get; private set; }
		public Currency Currency { get; private set; }

		public BankAccount(
			BankAccountId id,
			UserId userId,
			CompanyId companyId,
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
			)
		{
			Id = id;
			UserId = userId;
			CompanyId = companyId;
			Name = name;
			BankName = bankName;
			BankAccountNumber = bankAccountNumber;
			Currency = currency;
		}

		public static BankAccount Create(
			UserId userId,
			CompanyId companyId,
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
			)
		{
			var bankAccount = new BankAccount(
				new BankAccountId(Guid.NewGuid()),
				userId,
				companyId,
				name,
				bankName,
				bankAccountNumber,
				currency
			);

			return bankAccount;
		}

		public void Update(
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
			)
		{
			Name = name;
			BankName = bankName;
			BankAccountNumber = bankAccountNumber;
			Currency = currency;
		}
	}
}

