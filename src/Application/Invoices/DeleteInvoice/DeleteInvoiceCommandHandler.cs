using Application.Configuration.Commands;
using Domain.Invoices;

namespace Application.Invoices.DeleteInvoice;

public class DeleteInvoiceCommandHandler : ICommandHandler<DeleteInvoiceCommand>
{
    private readonly IInvoiceRepository _repository;
    
    public DeleteInvoiceCommandHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(new InvoiceId(request.Id), request.UserId);
    }
}