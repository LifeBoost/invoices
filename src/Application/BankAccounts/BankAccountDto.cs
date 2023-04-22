using System;
using Domain;
using Domain.Companies;

namespace Application.BankAccounts
{
	public class BankAccountDto
	{
		public Guid Id { get; }
		public Guid CompanyId { get; }
		public string Name { get; }
		public string BankName { get; }
		public string BankAccountNumber { get; }
		public Currency Currency { get; }

		public BankAccountDto(
			Guid id,
			Guid companyId,
			string name,
			string bankName,
			string bankAccountNumber,
			Currency currency
			)
		{
			Id = id;
			CompanyId = companyId;
			Name = name;
			BankName = bankName;
			BankAccountNumber = bankAccountNumber;
			Currency = currency;
		}
	}
}

