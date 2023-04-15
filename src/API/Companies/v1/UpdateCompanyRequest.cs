using System.ComponentModel.DataAnnotations;
using Domain.Companies;

namespace API.Companies.v1;

public class UpdateCompanyRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string IdentificationNumber { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public bool IsVatPayer { get; set; }
    
    [EnumDataType(typeof(VatRejectionReason))]
    public VatRejectionReason? VatRejectionReason { get; set; }

    [Required]
    [StringLength(255)]
    public string Street { get; set; }

    [Required]
    [StringLength(255)]
    public string City { get; set; }

    [Required]
    [StringLength(10)]
    public string ZipCode { get; set; }
}