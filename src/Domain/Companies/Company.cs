using Domain.Addresses;
using Domain.SeedWork;
using Domain.Users;

namespace Domain.Companies;

public class Company : Entity, IAggregateRoot
{
    public CompanyId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Name { get; private set; }
    public string IdentificationNumber { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool IsVatPayer { get; private set; }
    public int? VatRejectionReason { get; private set; }
    public Address Address { get; private set; }

    private Company(
        UserId userId,
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        int? vatRejectionReason,
        Address address
    )
    {
        this.Id = new CompanyId(Guid.NewGuid());
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Email = email;
        PhoneNumber = phoneNumber;
        IsVatPayer = isVatPayer;
        VatRejectionReason = vatRejectionReason;
        Address = address;
    }

    public static Company Create(
        UserId userId,
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        int? vatRejectionReason,
        Address address
    )
    {
        return new Company(
            userId,
            name,
            identificationNumber,
            email,
            phoneNumber,
            isVatPayer,
            vatRejectionReason,
            address
        );
    }
}