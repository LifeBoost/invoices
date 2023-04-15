using Application.Configuration.Commands;
using Domain.Companies;

namespace Application.Companies.UpdateCompany;

public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, ICompanyUniquenessChecker companyUniquenessChecker)
    {
        _companyRepository = companyRepository;
        _companyUniquenessChecker = companyUniquenessChecker;
    }

    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyId = new CompanyId(request.Id);
        var company = await _companyRepository.GetByIdAsync(companyId, request.UserId);

        company.Address.Update(request.Street, request.City, request.ZipCode);
        company.Update(
            request.Name,
            request.IdentificationNumber,
            request.Email,
            request.PhoneNumber,
            request.IsVatPayer,
            request.VatRejectionReason,
            _companyUniquenessChecker
        );

        await _companyRepository.SaveAsync(company);
    }
}