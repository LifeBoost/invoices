namespace Domain.Companies;

public interface ICompanyUniquenessChecker
{
    public bool IsUnique(string name, string identificationNumber);
}