using Application.Configuration.Commands;
using Domain.Invoices;
using Domain.Users;

namespace Application.Invoices.CreateInvoice;

public class CreateInvoiceCommand : CommandBase<InvoiceDto>
{
    public UserId UserId { get; }
    public string JsonParameters { get; }

    public CreateInvoiceCommand(UserId userId, string jsonParameters)
    {
        UserId = userId;
        JsonParameters = jsonParameters;
    }
}
