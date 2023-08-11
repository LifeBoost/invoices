using Domain.Companies;
using Domain.Contractors;
using Domain.SeedWork;
using Domain.Users;

namespace Domain.Invoices;

public class Invoice : Entity, IAggregateRoot
{
    public InvoiceId Id { get; private set; }
    public UserId UserId { get; private set; }
    public Status Status { get; private set; }
    public string JsonParameters { get; private set; }

    public Invoice(
        InvoiceId id,
        UserId userId,
        Status status,
        string jsonParameters
    )
    {
        Id = id;
        UserId = userId;
        Status = status;
        JsonParameters = jsonParameters;
    }

    public static Invoice Create(UserId userId, string jsonParameters)
    {
        var invoice = new Invoice(
            new InvoiceId(Guid.NewGuid()),
            userId,
            Status.Draft,
            jsonParameters
        );

        return invoice;
    }

    public void Update(string jsonParameters)
    {
        JsonParameters = jsonParameters;
    }
}
