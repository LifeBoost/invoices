using Application.Configuration.Queries;
using Dapper;
using Infrastructure.Database;
using Infrastructure.Domain;
using Infrastructure.Domain.Invoices;

namespace Application.Invoices.GetAllInvoices;

public class GetAllInvoicesQueryHandler : IQueryHandler<GetAllInvoicesQuery, List<InvoiceDto>>
{
    private readonly DapperContext _context;
    
    public GetAllInvoicesQueryHandler(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<List<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
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
            WHERE i.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var rows = await connection.QueryAsync(sql, new { UserId = request.UserId.Value.ToString() });

        return rows.Select(
            row => new InvoiceDto(
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
            )
        ).ToList();
    }
}