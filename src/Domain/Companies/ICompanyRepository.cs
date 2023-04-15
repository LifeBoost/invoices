using Domain.Users;

namespace Domain.Companies;

public interface ICompanyRepository
{
    Task<Company> GetByIdAsync(CompanyId id, UserId userId);

    Task AddAsync(Company company);

    Task DeleteAsync(CompanyId id, UserId userId);

    Task SaveAsync(Company company);
}