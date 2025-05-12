using FluentValidation;

namespace Web.App.Validators;

public class OrganisationIdentifierValidator : AbstractValidator<OrganisationIdentifier>
{
    private readonly string[] _organisationTypes = [OrganisationTypes.School, OrganisationTypes.Trust, OrganisationTypes.LocalAuthority];

    public OrganisationIdentifierValidator()
    {
        RuleFor(p => p.Value)
            .NotEmpty()
            .WithMessage("Organisation identifier must be provided");

        RuleFor(p => p.Type)
            .NotEmpty()
            .WithMessage("Organisation type must be provided");

        RuleFor(p => p.Type)
            .Must(type => _organisationTypes.Contains(type))
            .When(p => p.Type != null)
            .WithMessage("Organisation type must be valid");

        RuleFor(p => p.Value)
            .Length(6)
            .When(p => p.Type == OrganisationTypes.School)
            .When(p => p.ValueAsInt != null)
            .WithMessage("School URN must be 6 characters");

        RuleFor(p => p.Value)
            .Length(8)
            .When(p => p.Type == OrganisationTypes.Trust)
            .When(p => p.ValueAsInt != null)
            .WithMessage("Trust company number must be 8 characters");

        RuleFor(p => p.Value)
            .Length(3)
            .When(p => p.Type == OrganisationTypes.LocalAuthority)
            .When(p => p.ValueAsInt != null)
            .WithMessage("Local authority code must be 3 characters");

        RuleFor(p => p.ValueAsInt)
            .NotEmpty()
            .When(p => p.Value != null)
            .WithMessage("Organisation identifier must be a number");
    }
}

public class OrganisationIdentifier
{
    public string? Value { get; init; }
    public string? Type { get; init; }
    internal int? ValueAsInt
    {
        get
        {
            if (int.TryParse(Value!, out var value))
            {
                return value;
            }

            return null;
        }
    }
}