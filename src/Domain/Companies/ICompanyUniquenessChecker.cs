namespace Domain.Companies;

public interface ICompanyUniquenessChecker
{
    public bool IsUniqueName(string name);

    public bool IsUniqueIdentificationNumber(string identificationNumber);
}