using System.Data;
using Dapper;
using Domain.BankAccounts;
using Domain.BankAccounts.Exceptions;
using Domain.Companies;
using Domain.Users;
using Infrastructure.Database;

namespace Infrastructure.Domain.BankAccounts;

public class BankAccountMysqlDatabaseRepository : IBankAccountRepository
{
    private readonly DapperContext _context;

    public BankAccountMysqlDatabaseRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<BankAccount> GetByIdAsync(BankAccountId id, UserId userId)
    {
        const string sql = @"
            SELECT
                id AS Id,
                users_id AS UserId,
                companies_id AS CompanyId,
                name AS Name,
                bank_name AS BankName,
                bank_account_number AS BankAccountNumber,
                currency_code AS Currency
            FROM bank_accounts 
            WHERE id = @Id AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });

        if (row == null)
        {
            throw new BankAccountNotFoundException();
        }

        return new BankAccount(
            new BankAccountId(new Guid(row.Id)),
            new UserId(new Guid(row.UserId)),
            new CompanyId(new Guid(row.CompanyId)),
            row.Name,
            row.BankName,
            row.BankAccountNumber,
            CurrencyFormatter.ToEnum(row.Currency)
        );
    }

    public async Task AddAsync(BankAccount bankAccount)
    {
        const string sql = @"
            INSERT INTO bank_accounts 
                (id, users_id, companies_id, name, bank_name, bank_account_number, currency_code, created_at)
            VALUES 
                (@Id, @UserId, @CompanyId, @Name, @BankName, @BankAccountNumber, @Currency, @CreatedAt);
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", bankAccount.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", bankAccount.UserId.Value.ToString(), DbType.String);
        parameters.Add("CompanyId", bankAccount.CompanyId.Value.ToString(), DbType.String);
        parameters.Add("Name", bankAccount.Name, DbType.String);
        parameters.Add("BankName", bankAccount.BankName, DbType.String);
        parameters.Add("BankAccountNumber", bankAccount.BankAccountNumber, DbType.String);
        parameters.Add("Currency", CurrencyFormatter.ToDatabaseValue(bankAccount.Currency), DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.Date);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(BankAccountId id, UserId userId)
    {
        const string sql = @"
            DELETE FROM bank_accounts WHERE id = @Id AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });
    }

    public async Task SaveAsync(BankAccount bankAccount)
    {
        const string sql = @"
            UPDATE
                bank_accounts
            SET
                name = @Name,
                bank_name = @BankName,
                bank_account_number = @BankAccountNumber,
                currency_code = @Currency
            WHERE
                id = @Id AND users_id = @UserId;
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", bankAccount.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", bankAccount.UserId.Value.ToString(), DbType.String);
        parameters.Add("Name", bankAccount.Name, DbType.String);
        parameters.Add("BankName", bankAccount.BankName, DbType.String);
        parameters.Add("BankAccountNumber", bankAccount.BankAccountNumber, DbType.String);
        parameters.Add("Currency", CurrencyFormatter.ToDatabaseValue(bankAccount.Currency), DbType.String);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }
}