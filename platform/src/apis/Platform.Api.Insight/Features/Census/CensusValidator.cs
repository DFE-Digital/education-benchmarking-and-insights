using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Census;

public interface ICensusValidator
{
    Task<ValidationResult> ValidateAsync(CensusParameters parameters, CancellationToken token = default);
    Task<ValidationResult> ValidateAsync(CensusQuerySchoolsParameters parameters, CancellationToken token = default);
    Task<ValidationResult> ValidateAsync(CensusNationalAvgParameters parameters, CancellationToken token = default);
}

[ExcludeFromCodeCoverage]
public class CensusValidator : ICensusValidator
{
    private readonly CensusParametersValidator _paramsValidator = new();
    private readonly CensusQuerySchoolsParametersValidator _querySchoolsParamsValidator = new();
    private readonly CensusNationalAvgParametersValidator _nationalAvgParamsValidator = new();
    public Task<ValidationResult> ValidateAsync(CensusParameters parameters, CancellationToken token = default)
    {
        return _paramsValidator.ValidateAsync(parameters, token);
    }

    public Task<ValidationResult> ValidateAsync(CensusQuerySchoolsParameters parameters, CancellationToken token = default)
    {
        return _querySchoolsParamsValidator.ValidateAsync(parameters, token);
    }

    public Task<ValidationResult> ValidateAsync(CensusNationalAvgParameters parameters, CancellationToken token = default)
    {
        return _nationalAvgParamsValidator.ValidateAsync(parameters, token);
    }
}

[ExcludeFromCodeCoverage]
public class CensusParametersValidator : AbstractValidator<CensusParameters>
{
    public CensusParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Categories.Census.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Census.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || Categories.Census.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => Dimensions.Census.IsValid(dimension);
}

[ExcludeFromCodeCoverage]
public class CensusQuerySchoolsParametersValidator : AbstractValidator<CensusQuerySchoolsParameters>
{
    public CensusQuerySchoolsParametersValidator()
    {
        Include(new CensusParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}

[ExcludeFromCodeCoverage]
public class CensusNationalAvgParametersValidator : AbstractValidator<CensusNationalAvgParameters>
{
    public CensusNationalAvgParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Census.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Census.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}