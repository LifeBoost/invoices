using Application.Companies.CreateCompany;
using Application.Configuration.Commands;
using Domain.Companies;
using Domain.Users;

namespace Application.Companies.UpdateCompany;

public class UpdateCompanyCommand : CommandBase
{
    public Guid Id { get; }
    public UserId UserId { get; }
    public string Name { get; }
    public string IdentificationNumber { get; }
    public string? Email { get; }
    public string? PhoneNumber { get; }
    public bool IsVatPayer { get; }
    public VatRejectionReason? VatRejectionReason { get; }
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }

    public UpdateCompanyCommand(
        Guid id,
        UserId userId,
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        VatRejectionReason? vatRejectionReason,
        string street,
        string city,
        string zipCode
    )
    {
        Id = id;
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Email = email;
        PhoneNumber = phoneNumber;
        IsVatPayer = isVatPayer;
        VatRejectionReason = vatRejectionReason;
        Street = street;
        City = city;
        ZipCode = zipCode;
    }
}