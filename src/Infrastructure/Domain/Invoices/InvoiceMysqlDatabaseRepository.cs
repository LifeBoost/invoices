using System.Data;
using Dapper;
using Domain.Invoices;
using Domain.Invoices.Exceptions;
using Domain.Users;
using Infrastructure.Database;

namespace Infrastructure.Domain.Invoices;

public class InvoiceMysqlDatabaseRepository : IInvoiceRepository
{
    private readonly DapperContext _context;
    
    public InvoiceMysqlDatabaseRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Invoice> GetByIdAsync(InvoiceId id, UserId userId)
    {
        const string sql = @"
            SELECT 
                id AS Id,
                users_id AS UserId,
                status AS Status,
                json_parameters AS JsonParameters
            FROM invoices 
            WHERE id = @Id AND users_id = @UserId; 
        ";

        using var connection = _context.CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });

        if (row == null)
        {
            throw new InvoiceNotFoundException();
        }

        return new Invoice(
            new InvoiceId(new Guid(row.Id)),
            new UserId(new Guid(row.UserId)),
            StatusFormatter.ToEnum(row.Status),
            row.JsonParameters
        );
    }

    public async Task AddAsync(Invoice invoice)
    {
        const string sql = @"
            INSERT INTO invoices 
                (id, users_id, status, json_parameters, created_at) 
            VALUES 
                (@Id, @UserId, @Status, @JsonParameters, @CreatedAt);
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", invoice.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", invoice.UserId.Value.ToString(), DbType.String);
        parameters.Add("Status", StatusFormatter.ToDatabaseValue(invoice.Status), DbType.String);
        parameters.Add("JsonParameters", invoice.JsonParameters, DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.DateTime);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);

        // TODO: save invoice parameters 
    }

    public async Task SaveAsync(Invoice invoice)
    {
        const string sql = @"
            UPDATE invoices 
            SET
                status = @Status,
                json_parameters = @JsonParameters
            WHERE id = @Id AND users_id = @UserId;
        ";
        
        var parameters = new DynamicParameters();
        parameters.Add("Status", StatusFormatter.ToDatabaseValue(invoice.Status), DbType.String);
        parameters.Add("JsonParameters", invoice.JsonParameters, DbType.String);
        parameters.Add("Id", invoice.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", invoice.UserId.Value.ToString(), DbType.String);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(InvoiceId id, UserId userId)
    {
        const string sql = @"
            DELETE FROM invoices WHERE id = @Id AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });
    }
}