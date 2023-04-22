using System.Data;
using Dapper;
using Domain.Addresses;
using Domain.Contractors;
using Domain.Contractors.Exceptions;
using Domain.Users;
using Infrastructure.Database;
using Infrastructure.Domain.Addresses;

namespace Infrastructure.Domain.Contractors;

public class ContractorMysqlDatabaseRepository : IContractorRepository
{
    private readonly DapperContext _context;
    private readonly AddressRepository _addressRepository;

    public ContractorMysqlDatabaseRepository(DapperContext context, AddressRepository addressRepository)
    {
        _context = context;
        _addressRepository = addressRepository;
    }
    
    public async Task<Contractor> GetByIdAsync(ContractorId id, UserId userId)
    {
        const string sql = @"
            SELECT
                c.id AS ContractorId,
                c.users_id AS UserId,
                c.addresses_id AS AddressId,
                c.name AS Name,
                c.identification_number AS IdentificationNumber,
                a.id AS AddressId,
                a.street AS Street,
                a.city AS City,
                a.zip_code AS ZipCode
            FROM contractors c
            JOIN addresses a ON a.id = c.addresses_id
            WHERE c.id = @Id AND c.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        var contractor = await connection.QuerySingleAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });

        if (contractor == null)
        {
            throw new ContractorNotFoundException();
        }

        return new Contractor(
            id,
            userId,
            contractor.Name,
            contractor.IdentificationNumber,
            new Address(
                new AddressId(new Guid(contractor.AddressId)),
                contractor.Street,
                contractor.City,
                contractor.ZipCode
            )
        );
    }

    public async Task AddAsync(Contractor contractor)
    {
        await _addressRepository.AddAsync(contractor.Address, contractor.UserId);

        const string sql = @"
            INSERT INTO contractors
                (id, users_id, addresses_id, name, identification_number, created_at)
            VALUES
                (@Id, @UserId, @AddressId, @Name, @IdentificationNumber, @CreatedAt);
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", contractor.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", contractor.UserId.Value.ToString(), DbType.String);
        parameters.Add("AddressId", contractor.Address.Id.Value.ToString(), DbType.String);
        parameters.Add("Name", contractor.Name, DbType.String);
        parameters.Add("IdentificationNumber", contractor.IdentificationNumber, DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.Date);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(ContractorId id, UserId userId)
    {
        const string sql = @"
            DELETE c, a
            FROM contractors c
            INNER JOIN addresses a ON a.id = c.addresses_id
            WHERE c.Id = @Id AND c.users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });
    }

    public async Task SaveAsync(Contractor contractor)
    {
        const string sql = @"
            UPDATE contractors 
            SET
                name = @Name,
                identification_number = @IdentificationNumber,
                updated_at = @UpdatedAt
            WHERE
                id = @Id AND users_id = @UserId;
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", contractor.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", contractor.UserId.Value.ToString(), DbType.String);
        parameters.Add("Name", contractor.Name, DbType.String);
        parameters.Add("IdentificationNumber", contractor.IdentificationNumber, DbType.String);
        parameters.Add("UpdatedAt", DateTime.Now, DbType.Date);

        await _addressRepository.SaveAsync(contractor.Address, contractor.UserId);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }
}