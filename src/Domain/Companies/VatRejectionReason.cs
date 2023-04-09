namespace Domain.Companies;

public enum VatRejectionReason
{
    ExemptionByLessThanMinimalAnnualTurnover = 1,
    ExemptionByMfDisposition = 2,
    ExemptionByTypeOfBusiness = 3
}