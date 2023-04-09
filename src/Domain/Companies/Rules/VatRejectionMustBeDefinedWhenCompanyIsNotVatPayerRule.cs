using Domain.SeedWork;

namespace Domain.Companies.Rules;

public class VatRejectionMustBeDefinedWhenCompanyIsNotVatPayerRule : IBusinessRule
{
    private readonly Company _company;

    public VatRejectionMustBeDefinedWhenCompanyIsNotVatPayerRule(Company company)
    {
        _company = company;
    }

    public bool IsBroken() => _company is { IsVatPayer: false, VatRejectionReason: null };

    public string Message => "VAT Rejection Reason must be defined when company is not VAT payer";
}