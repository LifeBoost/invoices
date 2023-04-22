using System;
using Application.Configuration.Commands;
using Domain.BankAccounts;

namespace Application.BankAccounts.DeleteBankAccount
{
	public class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand>
	{
        private readonly IBankAccountRepository _repository;

		public DeleteBankAccountCommandHandler(IBankAccountRepository repository)
		{
            _repository = repository;
		}

        public async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new BankAccountId(request.Id), request.UserId);
        }
    }
}

