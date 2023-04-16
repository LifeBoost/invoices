using System.Data;
using Dapper;
using Domain.Addresses;
using Domain.Companies;
using Domain.Users;
using Domain.Companies.Exceptions;
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

    public async Task<Company> GetByIdAsync(CompanyId id, UserId userId)
    {
        const string query = @"
            SELECT 
                c.id AS companyId,
                c.users_id AS userId,
                c.name AS name,
                c.identification_number AS identificationNumber, 
                c.is_vat_payer AS isVatPayer,
                c.vat_rejection_reason AS vatRejectionReason, 
                c.email AS email, 
                c.phone_number AS phoneNumber,
                a.id AS addressId,
                a.street AS street,
                a.city AS city,
                a.zip_code AS zipCode
            FROM companies c
            JOIN addresses a ON a.id = c.addresses_id
            WHERE c.id = @Id AND c.users_id = @UserId;
        ";
        using var connection = _context.CreateConnection();
        var company = await connection.QuerySingleAsync(query, new { Id = id.Value.ToString(), UserId = userId.Value.ToString() });

        if (company == null)
        {
            throw new CompanyNotFoundException();
        }

        return new Company(
            new CompanyId(new Guid(company.companyId)),
            new UserId(new Guid(company.userId)),
            company.name,
            company.identificationNumber,
            company.email,
            company.phoneNumber,
            company.isVatPayer,
            (VatRejectionReason?)company.vatRejectionReason,
            new Address(
                new AddressId(new Guid(company.addressId)),
                company.street,
                company.city,
                company.zipCode
            )
        );
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
        await connection.ExecuteAsync(query, new { Id = id, UserId = userId });
    }

    public async Task SaveAsync(Company company)
    {
        const string query = @"
            UPDATE companies 
            SET
                name = @Name,
                identification_number = @IdentificationNumber,
                is_vat_payer = @IsVatPayer,
                vat_rejection_reason = @VatRejectionReason,
                email = @Email,
                phone_number = @PhoneNumber,
                updated_at = @UpdatedAt
            WHERE id = @Id AND users_id = @UserId;
        ";
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", company.Id.Value.ToString(), DbType.String);
        parameters.Add("UserId", company.UserId.Value.ToString(), DbType.String);
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("IdentificationNumber", company.IdentificationNumber, DbType.String);
        parameters.Add("IsVatPayer", company.IsVatPayer, DbType.Boolean);
        parameters.Add("VatRejectionReason", company.VatRejectionReason, DbType.Int32);
        parameters.Add("Email", company.Email, DbType.String);
        parameters.Add("PhoneNumber", company.PhoneNumber, DbType.String);
        parameters.Add("UpdatedAt", DateTime.Now, DbType.Date);

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