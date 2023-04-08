using Application.Configuration.Commands;
using Domain.Addresses;
using Domain.Companies;
using Domain.Users;

namespace Application.Companies.CreateCompany;

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyDTO>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserContext _userContext;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUserContext userContext)
    {
        this._companyRepository = companyRepository;
        this._userContext = userContext;
    }

    public async Task<CompanyDTO> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = Company.Create(
            await _userContext.GetUserId(command.Token),
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
            )
        );

        await this._companyRepository.AddAsync(company);

        return new CompanyDTO{Id = company.Id.Value};
    }
}