using Application.Configuration.Commands;
using Domain.BankAccounts;
using Domain.Companies;

namespace Application.BankAccounts.CreateBankAccount
{
	public class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, BankAccountDto>
	{
		private readonly IBankAccountRepository _repository;

		public CreateBankAccountCommandHandler(IBankAccountRepository repository)
		{
			_repository = repository;
		}

        public async Task<BankAccountDto> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
			var bankAccount = BankAccount.Create(
				request.UserId,
                new CompanyId(request.CompanyId),
				request.Name,
				request.BankName,
				request.BankAccountNumber,
				request.Currency
			);

			await _repository.AddAsync(bankAccount);

			return new BankAccountDto() { Id = bankAccount.Id.Value };
        }
    }
}
