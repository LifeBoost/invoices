using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Invoices.GetInvoiceById;

public class GetInvoiceByIdQuery : IQuery<InvoiceDto>
{
    public Guid Id { get; }
    public UserId UserId { get; }

    public GetInvoiceByIdQuery(Guid id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}