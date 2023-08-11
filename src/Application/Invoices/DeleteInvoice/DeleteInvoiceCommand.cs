using System.Security.Principal;
using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Invoices.DeleteInvoice;

public class DeleteInvoiceCommand : CommandBase
{
    public Guid Id { get; }
    public UserId UserId { get; }

    public DeleteInvoiceCommand(Guid id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }
}