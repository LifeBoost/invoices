using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Contractors.CreateContractor;

public class CreateContractorCommand : CommandBase<ContractorDTO>
{
    public UserId UserId { get; }
    public string Name { get; }
    public string IdentificationNumber { get; }
    public string Street { get; }
    public string ZipCode { get; }
    public string City { get; }

    public CreateContractorCommand(
        UserId userId,
        string name, 
        string identificationNumber, 
        string street, 
        string zipCode, 
        string city
    )
    {
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Street = street;
        ZipCode = zipCode;
        City = city;
    }
}