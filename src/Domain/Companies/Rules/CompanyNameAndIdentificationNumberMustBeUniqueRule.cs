using Domain.SeedWork;

namespace Domain.Companies.Rules;

public class CompanyNameAndIdentificationNumberMustBeUniqueRule : IBusinessRule
{
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker;
    private readonly Company _company;
    
    public CompanyNameAndIdentificationNumberMustBeUniqueRule(ICompanyUniquenessChecker companyUniquenessChecker, Company company)
    {
        _companyUniquenessChecker = companyUniquenessChecker;
        _company = company;
    }

    public bool IsBroken() => !_companyUniquenessChecker.IsUnique(_company.Name, _company.IdentificationNumber);

    public string Message => "Company with current name or identification number already exists";
}