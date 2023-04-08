using Application.Configuration.Commands;
using MediatR;

namespace Application.Companies.CreateCompany;

public class CreateCompanyCommand : CommandBase<CompanyDTO>
{
    public string Token { get; }
    public string Name { get; }
    public string IdentificationNumber { get; }
    public string? Email { get; }
    public string? PhoneNumber { get; }
    public bool IsVatPayer { get; }
    public int? VatRejectionReason { get; }
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }

    public CreateCompanyCommand(
        string token,
        string name, 
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        int? vatRejectionReason,
        string street,
        string city,
        string zipCode
    )
    {
        this.Token = token;
        this.Name = name;
        this.IdentificationNumber = identificationNumber;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.IsVatPayer = isVatPayer;
        this.VatRejectionReason = vatRejectionReason;
        this.Street = street;
        this.City = city;
        this.ZipCode = zipCode;
    }
}