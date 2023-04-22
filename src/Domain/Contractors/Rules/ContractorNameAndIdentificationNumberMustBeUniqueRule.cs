using Domain.SeedWork;

namespace Domain.Contractors.Rules;

public class ContractorNameMustBeUniqueRule : IBusinessRule 
{
    
    private readonly IContractorUniquenessChecker _contractorUniquenessChecker;
    private readonly Contractor _contractor;
    
    public ContractorNameMustBeUniqueRule(
        IContractorUniquenessChecker contractorUniquenessChecker, 
        Contractor contractor
    )
    {
        _contractorUniquenessChecker = contractorUniquenessChecker;
        _contractor = contractor;
    }

    public bool IsBroken() => !_contractorUniquenessChecker.IsUniqueName(_contractor.Name, _contractor.UserId);

    public string Message => "Contractor with current name already exists";
}

public class ContractorIdentificationNumberMustBeUniqueRule : IBusinessRule 
{
    private readonly IContractorUniquenessChecker _contractorUniquenessChecker;
    private readonly Contractor _contractor;
    
    public ContractorIdentificationNumberMustBeUniqueRule(
        IContractorUniquenessChecker contractorUniquenessChecker, 
        Contractor contractor
    )
    {
        _contractorUniquenessChecker = contractorUniquenessChecker;
        _contractor = contractor;
    }

    public bool IsBroken() => !_contractorUniquenessChecker.IsUniqueIdentificationNumber(_contractor.IdentificationNumber, _contractor.UserId);

    public string Message => "Contractor with current identification number already exists";
}