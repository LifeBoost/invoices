using Application.Configuration.Queries;
using Dapper;
using Domain.Invoices.Exceptions;
using Infrastructure.Database;
using Infrastructure.Domain;
using Infrastructure.Domain.Invoices;

namespace Application.Invoices.GetInvoiceById;

public class GetInvoiceByIdQueryHandler : IQueryHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    private readonly DapperContext _context;
    
    public GetInvoiceByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT 
                i.id AS Id,
                i.status AS Status,
                ip.companies_id AS CompanyId,
                ip.contractors_id AS ContractorId,
                ip.number AS Number,
                ip.total_amount AS TotalAmount,
                ip.tax AS Tax,
                ip.currency AS Currency,
                ip.generated_at AS GeneratedAt,
                ip.sold_at AS SoldAt
            FROM invoices i
            JOIN invoice_parameters ip on i.id = ip.invoices_id
            WHERE i.id = @Id AND i.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(sql, new { Id = request.Id.ToString(), UserId = request.UserId.Value.ToString() });

        if (row == null)
        {
            throw new InvoiceNotFoundException();
        }

        return new InvoiceDto(
            new Guid(row.Id),
            StatusFormatter.ToEnum(row.Status),
            new Guid(row.CompanyId),
            new Guid(row.ContractorId),
            row.Number,
            row.TotalAmount,
            row.Tax,
            CurrencyFormatter.ToEnum(row.Currency),
            row.GeneratedAt,
            row.SoldAt
        );
    }
}