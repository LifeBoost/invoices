using Application.Configuration.Commands;
using Domain.Invoices;

namespace Application.Invoices.UpdateInvoice;

public class UpdateInvoiceCommandHandler : ICommandHandler<UpdateInvoiceCommand>
{
    private readonly IInvoiceRepository _repository;
    
    public UpdateInvoiceCommandHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(new InvoiceId(request.Id), request.UserId);

        invoice.Update(request.JsonParameters);

        await _repository.SaveAsync(invoice);
    }
}