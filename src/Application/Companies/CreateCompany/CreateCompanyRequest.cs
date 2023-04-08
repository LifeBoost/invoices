namespace Application.Companies.CreateCompany;

public class CreateCompanyRequest
{
    public string Name { get; set; }
    public string IdentificationNumber { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsVatPayer { get; set; }
    public int? VatRejectionReason { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}