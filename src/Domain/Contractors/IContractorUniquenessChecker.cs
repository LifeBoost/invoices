using Domain.Users;

namespace Domain.Contractors;

public interface IContractorUniquenessChecker
{
    public bool IsUniqueName(string name, UserId userId);

    public bool IsUniqueIdentificationNumber(string identificationNumber, UserId userId);
}