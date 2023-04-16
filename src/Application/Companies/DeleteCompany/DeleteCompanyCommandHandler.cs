using Application.Configuration.Commands;
using Domain.Companies;

namespace Application.Companies.DeleteCompany;

public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    
    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        _companyRepository.DeleteAsync(request.Id, request.UserId);
    }
}