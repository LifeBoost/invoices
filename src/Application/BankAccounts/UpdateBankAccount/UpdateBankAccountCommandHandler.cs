using System;
using Application.Configuration.Commands;
using Domain.BankAccounts;

namespace Application.BankAccounts.UpdateBankAccount
{
	public class UpdateBankAccountCommandHandler : ICommandHandler<UpdateBankAccountCommand>
	{
        private readonly IBankAccountRepository _repository;

		public UpdateBankAccountCommandHandler(IBankAccountRepository repository)
		{
            _repository = repository;
		}

        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _repository.GetByIdAsync(new BankAccountId(request.Id), request.UserId);

            bankAccount.Update(
                request.Name,
                request.BankName,
                request.BankAccountNumber,
                request.Currency
            );

            await _repository.SaveAsync(bankAccount);
        }
    }
}

