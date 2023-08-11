using Application.Configuration.Commands;
using Domain.Users;

namespace Application.Invoices.UpdateInvoice;

public class UpdateInvoiceCommand : CommandBase
{
    public Guid Id { get; }
    public UserId UserId { get; }
    public string JsonParameters { get; }

    public UpdateInvoiceCommand(Guid id, UserId userId, string jsonParameters)
    {
        Id = id;
        UserId = userId;
        JsonParameters = jsonParameters;
    }
}
