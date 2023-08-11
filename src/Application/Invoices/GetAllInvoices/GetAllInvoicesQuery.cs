using Application.Configuration.Queries;
using Domain.Users;

namespace Application.Invoices.GetAllInvoices;

public class GetAllInvoicesQuery : IQuery<List<InvoiceDto>>
{
    public UserId UserId { get; }

    public GetAllInvoicesQuery(UserId userId)
    {
        UserId = userId;
    }
}
