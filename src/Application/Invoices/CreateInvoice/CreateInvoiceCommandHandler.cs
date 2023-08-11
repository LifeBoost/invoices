using Application.Configuration.Commands;
using Domain.Invoices;

namespace Application.Invoices.CreateInvoice;

public class CreateInvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IInvoiceRepository _repository;
    
    public CreateInvoiceCommandHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = Invoice.Create(request.UserId, request.JsonParameters);

        await _repository.AddAsync(invoice);

        return new InvoiceDto() { Id = invoice.Id.Value };
    }
}