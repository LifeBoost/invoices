using System.Windows.Input;
using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Contractors.UpdateContractor;

public class UpdateContractorCommand : CommandBase
{
    public Guid ContractorId { get; }
    
    public UserId UserId { get; }
    
    public string Name { get; }
    
    public string IdentificationNumber { get; }
    
    public string Street { get; }
    
    public string City { get; }
    
    public string ZipCode { get; }

    public UpdateContractorCommand(
        Guid contractorId,
        UserId userId,
        string name,
        string identificationNumber,
        string street,
        string city,
        string zipCode
    )
    {
        ContractorId = contractorId;
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Street = street;
        City = city;
        ZipCode = zipCode;
    }
}