using Application.Configuration.Queries;
using Dapper;
using Infrastructure.Database;
using Infrastructure.Domain;

namespace Application.BankAccounts.GetAllBankAccountsByCompanyId;

public class GetAllBankAccountsByCompanyIdQueryHandler : IQueryHandler<GetAllBankAccountsByCompanyIdQuery, List<BankAccountDto>>
{
    private readonly DapperContext _context;
    
    public GetAllBankAccountsByCompanyIdQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<List<BankAccountDto>> Handle(GetAllBankAccountsByCompanyIdQuery request, CancellationToken cancellationToken)
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
            WHERE companies_id = @CompanyId AND users_id = @UserId;
        ";

        var connection = _context.CreateConnection();
        var rows = await connection.QueryAsync(sql, new { CompanyId = request.CompanyId.ToString(), UserId = request.UserId.Value.ToString() });

        return rows.Select(row => new BankAccountDto(
                new Guid(row.Id),
                new Guid(row.CompanyId),
                row.Name,
                row.BankName,
                row.BankAccountNumber,
                CurrencyFormatter.ToEnum(row.Currency)
                ))
            .ToList();
    }
}