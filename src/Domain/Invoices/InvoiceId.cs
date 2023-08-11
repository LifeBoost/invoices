using Domain.SeedWork;

namespace Domain.Invoices;

public class InvoiceId : TypedIdValueBase
{
    public InvoiceId(Guid value) : base(value)
    {
    }
}