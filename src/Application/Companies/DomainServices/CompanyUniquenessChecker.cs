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
    
    public bool IsUniqueName(string name)
    {
        const string sql = @"
            SELECT 1 FROM companies WHERE name = @Name;
        ";
        using var connection = _context.CreateConnection();

        var result = connection.QueryFirstOrDefault<int?>(sql, new { Name = name});

        return result == null;
    }
    
    public bool IsUniqueIdentificationNumber(string identificationNumber)
    {
        const string sql = @"
            SELECT 1 FROM companies WHERE identification_number = @IdentificationNumber;
        ";
        using var connection = _context.CreateConnection();

        var result = connection.QueryFirstOrDefault<int?>(sql, new { IdentificationNumber = identificationNumber});

        return result == null;
    }
}