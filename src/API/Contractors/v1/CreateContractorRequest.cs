using System.ComponentModel.DataAnnotations;

namespace API.Contractors.v1;

public class CreateContractorRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string IdentificationNumber { get; set; }
    
    [Required]
    public string Street { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string ZipCode { get; set; }
}