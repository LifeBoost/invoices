namespace Domain.Companies;

public interface ICompanyRepository
{
    Task<Company> GetByIdAsync(CompanyId id);

    Task AddAsync(Company company);
}