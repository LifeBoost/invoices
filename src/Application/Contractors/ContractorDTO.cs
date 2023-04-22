namespace Application.Contractors;

public class ContractorDTO
{
    public Guid Id { get; }

    public string Name { get; }
    
    public string IdentificationNumber { get; }
    
    public string Street { get; }
    
    public string City { get; }
    
    public string ZipCode { get; }

    public ContractorDTO(
        Guid id,
        string name,
        string identificationNumber,
        string street,
        string city,
        string zipCode
    )
    {
        Id = id;
        Name = name;
        IdentificationNumber = identificationNumber;
        Street = street;
        City = city;
        ZipCode = zipCode;
    }
}