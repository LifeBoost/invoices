using Domain.Addresses;
using Domain.Contractors.Rules;
using Domain.SeedWork;
using Domain.Users;

namespace Domain.Contractors;

public class Contractor : Entity, IAggregateRoot
{
    public ContractorId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Name { get; private set; }
    public string IdentificationNumber { get; private set; }
    public Address Address { get; set; }

    public Contractor(
        ContractorId id,
        UserId userId,
        string name,
        string identificationNumber,
        Address address
    )
    {
        Id = id;
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Address = address;
    }

    public static Contractor Create(
        UserId userId,
        string name,
        string identificationNumber,
        Address address,
        IContractorUniquenessChecker contractorUniquenessChecker
    )
    {
        var contractor = new Contractor(
            new ContractorId(Guid.NewGuid()),
            userId,
            name,
            identificationNumber,
            address
        );

        CheckRule(new ContractorNameMustBeUniqueRule(contractorUniquenessChecker, contractor));
        CheckRule(new ContractorIdentificationNumberMustBeUniqueRule(contractorUniquenessChecker, contractor));

        return contractor;
    }

    public void Update(
        string name,
        string identificationNumber,
        IContractorUniquenessChecker contractorUniquenessChecker
    )
    {
        var isNameWasChanged = Name != name;
        var isIdentificationNumberWasChanged = IdentificationNumber != identificationNumber;
        
        Name = name;
        IdentificationNumber = identificationNumber;
        
        if (isNameWasChanged) CheckRule(new ContractorNameMustBeUniqueRule(contractorUniquenessChecker, this));
        if (isIdentificationNumberWasChanged) CheckRule(new ContractorIdentificationNumberMustBeUniqueRule(contractorUniquenessChecker, this));
    }
}