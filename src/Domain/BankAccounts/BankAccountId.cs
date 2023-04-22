using System;
using Domain.SeedWork;

namespace Domain.BankAccounts
{
	public class BankAccountId : TypedIdValueBase
	{
		public BankAccountId(Guid value) : base(value)
		{
		}
	}
}

