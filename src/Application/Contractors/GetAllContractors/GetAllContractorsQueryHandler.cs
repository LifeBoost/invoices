using Application.Configuration.Queries;
using Dapper;
using Infrastructure.Database;

namespace Application.Contractors.GetAllContractors;

public class GetAllContractorsQueryHandler : IQueryHandler<GetAllContractorsQuery, List<ContractorDTO>>
{
    private readonly DapperContext _context;
    
    public GetAllContractorsQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<List<ContractorDTO>> Handle(GetAllContractorsQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT
                c.id AS Id,
                c.name AS Name,
                c.identification_number AS IdentificationNumber,
                a.street AS Street,
                a.city AS City,
                a.zip_code AS ZipCode
            FROM contractors c
            JOIN addresses a on a.id = c.addresses_id
            WHERE c.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var rows = await connection.QueryAsync(sql, new { UserId = request.UserId.Value.ToString() });
        
        return rows.Select(
            row => new ContractorDTO(
                new Guid(row.Id),
                row.Name,
                row.IdentificationNumber, 
                row.Street, 
                row.ZipCode, 
                row.City
            )
        ).ToList();
    }
}