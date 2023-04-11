using Domain.Users;

namespace Domain.Companies;

public interface ICompanyRepository
{
    Task<Company> GetByIdAsync(CompanyId id);

    Task AddAsync(Company company);

    Task DeleteAsync(CompanyId id, UserId userId);
}