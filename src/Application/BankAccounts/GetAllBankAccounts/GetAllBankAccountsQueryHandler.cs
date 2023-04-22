using Application.Configuration.Queries;
using Dapper;
using Infrastructure.Database;
using Infrastructure.Domain;

namespace Application.BankAccounts.GetAllBankAccounts
{
	public class GetAllBankAccountsQueryHandler : IQueryHandler<GetAllBankAccountsQuery, List<BankAccountDto>>
	{
        private readonly DapperContext _context;

		public GetAllBankAccountsQueryHandler(DapperContext context)
		{
            _context = context;
		}

        public async Task<List<BankAccountDto>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    id AS Id,
                    companies_id AS CompanyId,
                    name AS Name,
                    bank_name AS BankName,
                    bank_account_number AS BankAccountNumber,
                    currency_code AS Currency
                FROM bank_accounts
                WHERE users_id = @UserId;
            ";

            using var connection = _context.CreateConnection();
            var bankAccount = await connection.QueryAsync(sql, new { UserId = request.UserId.Value.ToString() });

            return bankAccount.Select(
                row => 
                    new BankAccountDto(
                        new Guid(row.Id), 
                        new Guid(row.CompanyId), 
                        row.Name, 
                        row.BankName, 
                        row.BankAccountNumber,
                        CurrencyFormatter.ToEnum(row.Currency)
                    )
            ).ToList();
        }
    }
}
