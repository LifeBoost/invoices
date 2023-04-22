using System.Data;
using Dapper;
using Domain.Addresses;
using Domain.Users;
using Infrastructure.Database;

namespace Infrastructure.Domain.Addresses;

public class AddressRepository
{
    private readonly DapperContext _context;

    public AddressRepository(DapperContext context)
    {
        this._context = context ?? throw new ArgumentException(nameof(context));
    }
    
    public async Task AddAsync(Address address, UserId userId)
    {
        const string query = @"
            INSERT INTO addresses (id, users_id, name, street, zip_code, city, created_at)
            VALUES (@Id, @UserId, @Name, @Street, @ZipCode, @City, @CreatedAt)
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", address.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", userId.Value.ToString(), DbType.String);
        parameters.Add("Name", address.Street, DbType.String);
        parameters.Add("Street", address.Street, DbType.String);
        parameters.Add("ZipCode", address.ZipCode, DbType.String);
        parameters.Add("City", address.City, DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.Date);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task SaveAsync(Address address, UserId userId)
    {
        const string query = @"
            UPDATE addresses
            SET
                street = @Street,
                zip_code = @ZipCode,
                city = @City
            WHERE id = @Id AND users_id = @UserId;
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", address.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", userId.Value.ToString(), DbType.String);
        parameters.Add("Street", address.Street, DbType.String);
        parameters.Add("ZipCode", address.ZipCode, DbType.String);
        parameters.Add("City", address.City, DbType.String);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
}