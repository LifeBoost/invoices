using System.Data;
using Dapper;
using Domain.Companies;
using Domain.Users;
using Infrastructure.Database;
using Infrastructure.Domain.Addresses;

namespace Infrastructure.Domain.Companies;

public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext _context;
    private readonly AddressRepository _addressRepository;

    public CompanyRepository(DapperContext context, AddressRepository addressRepository)
    {
        this._context = context ?? throw new ArgumentException(nameof(context));
        this._addressRepository = addressRepository;
    }

    public async Task<Company> GetByIdAsync(CompanyId id)
    {
        const string query = "SELECT * FROM companies WHERE id = @Id";
        using var connection = _context.CreateConnection();

        var company = await connection.QuerySingleAsync<Company>(query, new { Id = id.ToString() });

        return company;
    }

    public async Task AddAsync(Company company)
    {
        await this._addressRepository.AddAsync(company.Address, company.UserId);

        const string query = @"
            INSERT INTO companies
            (id, users_id, addresses_id, name, identification_number, is_vat_payer, vat_rejection_reason, email, phone_number, created_at)
            VALUES
            (@Id, @UserId, @AddressId, @Name, @IdentificationNumber, @IsVatPayer, @VatRejectionReason, @Email, @PhoneNumber, @CreatedAt)
        ";

        var parameters = new DynamicParameters();
        parameters.Add("Id", company.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", company.UserId.Value.ToString(), DbType.String);
        parameters.Add("AddressId", company.Address.Id.Value.ToString(), DbType.String);
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("IdentificationNumber", company.IdentificationNumber, DbType.String);
        parameters.Add("IsVatPayer", company.IsVatPayer, DbType.Boolean);
        parameters.Add("VatRejectionReason", company.VatRejectionReason, DbType.Int32);
        parameters.Add("Email", company.Email, DbType.String);
        parameters.Add("PhoneNumber", company.PhoneNumber, DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.Date);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(CompanyId id, UserId userId)
    {
        const string query = @"
            DELETE FROM companies WHERE id = @Id AND users_id = @UserId;
        ";

        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });
    }
}