using Domain.Addresses;
using Domain.Companies.Rules;
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
    public VatRejectionReason? VatRejectionReason { get; private set; }
    public Address Address { get; set; }

    public Company(
        CompanyId companyId,
        UserId userId,
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        VatRejectionReason? vatRejectionReason,
        Address address
    )
    {
        Id = companyId;
        UserId = userId;
        Name = name;
        IdentificationNumber = identificationNumber;
        Email = email;
        PhoneNumber = phoneNumber;
        IsVatPayer = isVatPayer;
        VatRejectionReason = vatRejectionReason;
        Address = address;
    }

    private Company(
        UserId userId,
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        VatRejectionReason? vatRejectionReason,
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
        VatRejectionReason? vatRejectionReason,
        Address address,
        ICompanyUniquenessChecker companyUniquenessChecker
    )
    {
        var company = new Company(
            userId,
            name,
            identificationNumber,
            email,
            phoneNumber,
            isVatPayer,
            vatRejectionReason,
            address
        );
        
        CheckRule(new VatRejectionMustBeDefinedWhenCompanyIsNotVatPayerRule(company));
        CheckRule(new CompanyNameMustBeUniqueRule(companyUniquenessChecker, company));
        CheckRule(new CompanyIdentificationNumberMustBeUniqueRule(companyUniquenessChecker, company));

        return company;
    }

    public void Update(
        string name,
        string identificationNumber,
        string? email,
        string? phoneNumber,
        bool isVatPayer,
        VatRejectionReason? vatRejectionReason,
        ICompanyUniquenessChecker companyUniquenessChecker
    )
    {
        var isNameWasChanged = Name != name;
        var isIdentificationNumberWasChanged = IdentificationNumber != identificationNumber;

        Name = name;
        IdentificationNumber = identificationNumber;
        Email = email;
        PhoneNumber = phoneNumber;
        IsVatPayer = isVatPayer;
        VatRejectionReason = vatRejectionReason;

        CheckRule(new VatRejectionMustBeDefinedWhenCompanyIsNotVatPayerRule(this));
        if (isNameWasChanged) CheckRule(new CompanyNameMustBeUniqueRule(companyUniquenessChecker, this));
        if (isIdentificationNumberWasChanged) CheckRule(new CompanyIdentificationNumberMustBeUniqueRule(companyUniquenessChecker, this));
    }
}