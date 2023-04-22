using Application.Configuration.Commands;
using Domain.Contractors;

namespace Application.Contractors.DeleteContractor;

public class DeleteContractorCommandHandler : ICommandHandler<DeleteContractorCommand>
{
    private readonly IContractorRepository _repository;
    
    public DeleteContractorCommandHandler(IContractorRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(new ContractorId(request.ContractorId), request.UserId);
    }
}