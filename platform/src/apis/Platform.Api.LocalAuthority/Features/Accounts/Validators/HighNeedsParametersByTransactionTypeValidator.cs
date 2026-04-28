using FluentValidation;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.Accounts.Validators;

public class HighNeedsByTransactionTypeParametersValidator : AbstractValidator<HighNeedsByTransactionTypeParameters>
{
    public HighNeedsByTransactionTypeParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 30)
            .WithMessage("Between 1 and 30 local authority codes must be supplied");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.HighNeeds.Support)}");

        RuleFor(x => x.Type)
            .Must(BeAValidType)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", TransactionType.All)}");
    }

    private static bool BeAValidDimension(string dimension) => Dimensions.HighNeeds.IsValidSupport(dimension);
    private static bool BeAValidType(string type) => TransactionType.IsValid(type);
}