using Application.Configuration.Queries;
using Dapper;
using Infrastructure.Database;

namespace Application.Companies.GetAllCompanies;

public class GetAllCompaniesQueryHandler : IQueryHandler<GetAllCompaniesQuery, List<CompanyDTO>>
{
    private DapperContext _context;
    
    public GetAllCompaniesQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<List<CompanyDTO>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT
                c.id AS Id,
                c.name AS Name,
                c.identification_number AS IdentificationNumber,
                c.is_vat_payer AS IsVatPayer,
                c.vat_rejection_reason AS VatRejectionReason,
                c.email AS Email,
                c.phone_number AS PhoneNumber,
                a.street AS Street,
                a.zip_code AS ZipCode,
                a.city AS City
            FROM companies c
            JOIN addresses a on a.id = c.addresses_id
            WHERE c.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var rows = await connection.QueryAsync(sql, new {UserId = request.UserId.Value.ToString()});

        return rows.Select(
            row => new CompanyDTO(
                new Guid(row.Id), 
                row.Name, 
                row.IdentificationNumber, 
                row.IsVatPayer, 
                row.VatRejectionReason, 
                row.Email, 
                row.PhoneNumber, 
                row.Street, 
                row.ZipCode, 
                row.City
                )
            ).ToList();
    }
}