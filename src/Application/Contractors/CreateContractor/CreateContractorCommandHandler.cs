using Application.Configuration.Commands;
using Domain.Addresses;
using Domain.Contractors;

namespace Application.Contractors.CreateContractor;

public class CreateContractorCommandHandler : ICommandHandler<CreateContractorCommand, ContractorDTO>
{
    private readonly IContractorRepository _contractorRepository;
    private readonly IContractorUniquenessChecker _contractorUniquenessChecker;
    
    public CreateContractorCommandHandler(
        IContractorRepository contractorRepository,
        IContractorUniquenessChecker contractorUniquenessChecker
    )
    {
        _contractorRepository = contractorRepository;
        _contractorUniquenessChecker = contractorUniquenessChecker;
    }

    public async Task<ContractorDTO> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = Contractor.Create(
            request.UserId,
            request.Name,
            request.IdentificationNumber,
            Address.Create(request.Street, request.City, request.ZipCode),
            _contractorUniquenessChecker
        );

        await _contractorRepository.AddAsync(contractor);

        return new ContractorDTO() { Id = contractor.Id.Value };
    }
}