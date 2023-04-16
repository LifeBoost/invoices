using Domain.SeedWork;

namespace Domain.Companies.Rules;

public class CompanyNameMustBeUniqueRule : IBusinessRule
{
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker;
    private readonly Company _company;
    
    public CompanyNameMustBeUniqueRule(ICompanyUniquenessChecker companyUniquenessChecker, Company company)
    {
        _companyUniquenessChecker = companyUniquenessChecker;
        _company = company;
    }

    public bool IsBroken() => !_companyUniquenessChecker.IsUniqueName(_company.Name);

    public string Message => "Company with current name already exists";
}

public class CompanyIdentificationNumberMustBeUniqueRule : IBusinessRule
{
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker;
    private readonly Company _company;
    
    public CompanyIdentificationNumberMustBeUniqueRule(ICompanyUniquenessChecker companyUniquenessChecker, Company company)
    {
        _companyUniquenessChecker = companyUniquenessChecker;
        _company = company;
    }

    public bool IsBroken() => !_companyUniquenessChecker.IsUniqueIdentificationNumber(_company.IdentificationNumber);

    public string Message => "Company with current identification number already exists";
}