namespace Application.Companies;

public class CompanyDTO
{
    public Guid Id { get; }
    public string Name { get; }
    public string IdentificationNumber { get; }
    public bool IsVatPayer { get; }
    public int? VatRejectionReason { get; }
    public string? Email { get; }
    public string? PhoneNumber { get; }
    public string Street { get; }
    public string ZipCode { get; }
    public string City { get; }

    public CompanyDTO(
        Guid id,
        string name,
        string identificationNumber,
        bool isVatPayer,
        int? vatRejectionReason,
        string? email,
        string? phoneNumber,
        string street,
        string zipCode,
        string city
    )
    {
        Id = id;
        Name = name;
        IdentificationNumber = identificationNumber;
        IsVatPayer = isVatPayer;
        VatRejectionReason = vatRejectionReason;
        Email = email;
        PhoneNumber = phoneNumber;
        Street = street;
        ZipCode = zipCode;
        City = city;
    }
}