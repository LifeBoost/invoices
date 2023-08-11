﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace API.BankAccount.v1
{
	public class CreateBankAccountRequest
	{
		public Guid CompanyId { get; set; }
		
		public string Name { get; set; }
		
		public string BankName { get; set; }
		
		public string BankAccountNumber { get; set; }

		[EnumDataType(typeof(Currency))]
		public Currency Currency { get; set; }
	}
}

