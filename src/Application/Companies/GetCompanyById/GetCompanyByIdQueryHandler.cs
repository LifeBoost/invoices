using Application.Configuration.Queries;
using Dapper;
using Domain.Companies.Exceptions;
using Infrastructure.Database;

namespace Application.Companies.GetCompanyById;

public class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, CompanyDTO>
{
    private DapperContext _context;
    
    public GetCompanyByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<CompanyDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
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
            WHERE
                c.id = @Id
                AND c.users_id = @UserId
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(
            sql,
            new {Id = request.Id.ToString(), UserId = request.UserId.Value.ToString()}
        );

        if (row == null)
        {
            throw new CompanyNotFoundException();
        }

        return new CompanyDTO(
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
        );
    }
}