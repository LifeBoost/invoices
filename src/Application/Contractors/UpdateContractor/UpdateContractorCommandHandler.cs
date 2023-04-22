using Application.Configuration.Commands;
using Domain.Contractors;

namespace Application.Contractors.UpdateContractor;

public class UpdateContractorCommandHandler : ICommandHandler<UpdateContractorCommand>
{
    private readonly IContractorRepository _repository;
    private readonly IContractorUniquenessChecker _contractorUniquenessChecker;

    public UpdateContractorCommandHandler(
        IContractorRepository repository, 
        IContractorUniquenessChecker contractorUniquenessChecker
    )
    {
        _repository = repository;
        _contractorUniquenessChecker = contractorUniquenessChecker;
    }
    
    public async Task Handle(UpdateContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _repository.GetByIdAsync(new ContractorId(request.ContractorId), request.UserId);

        contractor.Address.Update(request.Street, request.City, request.ZipCode);

        contractor.Update(
            request.Name,
            request.IdentificationNumber,
            _contractorUniquenessChecker
        );

        await _repository.SaveAsync(contractor);
    }
}