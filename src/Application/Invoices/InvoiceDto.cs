using Domain;
using Domain.Invoices;

namespace Application.Invoices;

public class InvoiceDto
{
    public Guid Id { get; }
    public Status Status { get; }
    public Guid? CompanyId { get; }
    public Guid? ContractorId { get; }
    public string? Number { get; }
    public int? TotalAmount { get; }
    public int? Tax { get; }
    public Currency? Currency { get; }
    public DateTime? GeneratedAt { get; }
    public DateTime? SoldAt { get; }

    public InvoiceDto(
        Guid id,
        Status status,
        Guid? companyId,
        Guid? contractorId,
        string? number,
        int? totalAmount,
        int? tax,
        Currency? currency,
        DateTime? generatedAt,
        DateTime? soldAt
    )
    {
        Id = id;
        Status = status;
        CompanyId = companyId;
        ContractorId = contractorId;
        Number = number;
        TotalAmount = totalAmount;
        Tax = tax;
        Currency = currency;
        GeneratedAt = generatedAt;
        SoldAt = soldAt;
    }
}