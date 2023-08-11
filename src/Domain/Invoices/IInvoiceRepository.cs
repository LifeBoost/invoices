using Domain.Users;

namespace Domain.Invoices;

public interface IInvoiceRepository
{
    public Task<Invoice> GetByIdAsync(InvoiceId id, UserId userId);

    public Task AddAsync(Invoice invoice);

    public Task SaveAsync(Invoice invoice);

    public Task DeleteAsync(InvoiceId id, UserId userId);
}
