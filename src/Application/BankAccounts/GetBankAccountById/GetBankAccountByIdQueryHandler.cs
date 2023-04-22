using Application.Configuration.Queries;
using Dapper;
using Domain.BankAccounts.Exceptions;
using Infrastructure.Database;
using Infrastructure.Domain;

namespace Application.BankAccounts.GetBankAccountById;

public class GetBankAccountByIdQueryHandler : IQueryHandler<GetBankAccountByIdQuery, BankAccountDto>
{
    private readonly DapperContext _context;
    
    public GetBankAccountByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<BankAccountDto> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT
                id AS Id,
                companies_id AS CompanyId,
                name as Name,
                bank_name AS BankName,
                bank_account_number AS BankAccountNumber,
                currency_code AS Currency
            FROM bank_accounts
            WHERE id = @Id AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(sql, new { Id = request.Id.ToString(), UserId = request.UserId.Value.ToString() });

        if (row == null)
        {
            throw new BankAccountNotFoundException();
        }

        return new BankAccountDto(
            new Guid(row.Id),
            new Guid(row.CompanyId),
            row.Name,
            row.BankName,
            row.BankAccountNumber,
            CurrencyFormatter.ToEnum(row.Currency)
        );
    }
}