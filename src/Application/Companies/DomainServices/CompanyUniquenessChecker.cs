using Dapper;
using Domain.Companies;
using Infrastructure.Database;

namespace Application.Companies.DomainServices;

public class CompanyUniquenessChecker : ICompanyUniquenessChecker
{
    private readonly DapperContext _context;
    
    public CompanyUniquenessChecker(DapperContext context)
    {
        _context = context;
    }
    
    public bool IsUnique(string name, string identificationNumber)
    {
        const string sql = @"
            SELECT 1 FROM companies WHERE name = @Name OR identification_number = @IdentificationNumber;
        ";
        using var connection = _context.CreateConnection();

        var result = connection.QueryFirstOrDefault<int?>(sql, new { Name = name, IdentificationNumber = identificationNumber });

        return result == null;
    }
}