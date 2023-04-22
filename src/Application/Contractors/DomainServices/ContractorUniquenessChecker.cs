using Dapper;
using Domain.Contractors;
using Domain.Users;
using Infrastructure.Database;

namespace Application.Contractors.DomainServices;

public class ContractorUniquenessChecker : IContractorUniquenessChecker
{
    private readonly DapperContext _context;

    public ContractorUniquenessChecker(DapperContext context)
    {
        _context = context;
    }
    
    public bool IsUniqueName(string name, UserId userId)
    {
        const string sql = @"
            SELECT 1 FROM contractors WHERE name = @Name AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var result = connection.QueryFirstOrDefault<int?>(sql, new { Name = name, UserId = userId.Value.ToString() });

        return result == null;
    }

    public bool IsUniqueIdentificationNumber(string identificationNumber, UserId userId)
    {
        const string sql = @"
            SELECT 1 FROM contractors WHERE identification_number = @IdentificationNumber AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var result = connection.QueryFirstOrDefault<int?>(
            sql,
            new { IdentificationNumber = identificationNumber, UserId = userId.Value.ToString() }
        );

        return result == null;
    }
}