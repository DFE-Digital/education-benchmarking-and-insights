using FluentValidation;
using Platform.Api.Establishment.Features.LocalAuthorities.Parameters;
using Platform.Domain;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Validators;

public class LocalAuthoritiesNationalRankParametersValidator : AbstractValidator<LocalAuthoritiesNationalRankParameters>
{
    public LocalAuthoritiesNationalRankParametersValidator()
    {
        RuleFor(x => x.Ranking)
            .Must(BeAValidRanking)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfBudget)}");
        RuleFor(x => x.Sort)
            .Must(BeAValidSort!)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Ranking.Sort.All)}");
    }

    private static bool BeAValidRanking(string ranking) => Ranking.LocalAuthorityNationalRanking.IsValid(ranking);
    private static bool BeAValidSort(string sort) => Ranking.Sort.IsValid(sort);
}