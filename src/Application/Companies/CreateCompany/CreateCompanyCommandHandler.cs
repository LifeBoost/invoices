using Application.Configuration.Commands;
using Domain.Addresses;
using Domain.Companies;

namespace Application.Companies.CreateCompany;

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyDTO>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, ICompanyUniquenessChecker companyUniquenessChecker)
    {
        _companyRepository = companyRepository;
        _companyUniquenessChecker = companyUniquenessChecker;
    }

    public async Task<CompanyDTO> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = Company.Create(
            command.UserId,
            command.Name,
            command.IdentificationNumber,
            command.Email,
            command.PhoneNumber,
            command.IsVatPayer,
            command.VatRejectionReason,
            Address.Create(
                command.Street,
                command.City,
                command.ZipCode
            ),
            _companyUniquenessChecker
        );

        await this._companyRepository.AddAsync(company);

        return new CompanyDTO{Id = company.Id.Value};
    }
}