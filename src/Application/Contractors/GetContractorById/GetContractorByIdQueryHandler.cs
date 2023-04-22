using Application.Configuration.Queries;
using Dapper;
using Domain.Contractors.Exceptions;
using Infrastructure.Database;

namespace Application.Contractors.GetContractorById;

public class GetContractorByIdQueryHandler : IQueryHandler<GetContractorByIdQuery, ContractorDTO>
{
    private readonly DapperContext _context;
    
    public GetContractorByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<ContractorDTO> Handle(GetContractorByIdQuery request, CancellationToken cancellationToken)
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
            WHERE c.id = @Id AND c.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(sql, new { Id = request.ContractorId.ToString(), UserId = request.UserId.Value.ToString() });

        if (row == null)
        {
            throw new ContractorNotFoundException();
        }

        return new ContractorDTO(
            new Guid(row.Id),
            row.Name,
            row.IdentificationNumber,
            row.Street,
            row.City,
            row.ZipCode
        );
    }
}