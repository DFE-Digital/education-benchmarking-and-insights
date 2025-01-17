using FluentValidation;
using Platform.Domain;

namespace Platform.Api.Insight.Census;

public class CensusParametersValidator : AbstractValidator<CensusParameters>
{
    public CensusParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusCategories.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusDimensions.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || CensusCategories.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => CensusDimensions.IsValid(dimension);
}

public class QuerySchoolCensusParametersValidator : AbstractValidator<QuerySchoolCensusParameters>
{
    public QuerySchoolCensusParametersValidator()
    {
        Include(new CensusParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}

public class CensusNationalAvgParametersValidator : AbstractValidator<CensusNationalAvgParameters>
{
    public CensusNationalAvgParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusDimensions.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => CensusDimensions.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}