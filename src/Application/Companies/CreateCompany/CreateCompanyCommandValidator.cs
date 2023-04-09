using FluentValidation;

namespace Application.Companies.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.IdentificationNumber).NotEmpty().MaximumLength(10);
    }
}