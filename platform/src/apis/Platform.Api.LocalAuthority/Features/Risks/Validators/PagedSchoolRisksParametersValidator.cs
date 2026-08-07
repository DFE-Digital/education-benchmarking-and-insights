using System;
using System.Linq;
using FluentValidation;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.Risks.Validators;

public class PagedSchoolRisksParametersValidator : AbstractValidator<PagedSchoolRisksParameters>
{
    public PagedSchoolRisksParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 30)
            .WithMessage("Between 1 and 30 local authority codes must be supplied");

        RuleFor(x => x.Page)
            .Must(integer => integer is >= 1 and <= 100)
            .WithMessage("{{PropertyName}} must be an int >=1 and <= 100");

        RuleFor(x => x.PageSize)
            .Must(integer => integer >= 1)
            .WithMessage("{{PropertyName}} must be an int >=1");

        RuleFor(x => x.SortField)
            .Must(BeEmptyOrAValidSortByField)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidSortByFields)}");

        RuleFor(x => x.SortOrder)
            .Must(BeEmptyOrAValidSortByDirection)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidSortByDirections)}");

        RuleFor(x => x.Phase)
            .Must(BeEmptyOrAValidPhase)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidPhases)}");
    }

    internal static readonly string[] ValidSortByFields = LocalAuthoritySchoolRisksHeaders.All.ToArray();
    internal static readonly string[] ValidSortByDirections = SortDirection.All.ToArray();

    internal static readonly string[] ValidPhases =
    [
        OverallPhase.Nursery,
        OverallPhase.Primary,
        OverallPhase.Secondary,
        OverallPhase.Special,
        OverallPhase.PupilReferralUnit,
        OverallPhase.AllThrough
    ];

    private static bool BeEmptyOrAValidSortByField(string? field) =>
        field is null || ValidSortByFields.Any(f => f.Equals(field, StringComparison.OrdinalIgnoreCase));

    private static bool BeEmptyOrAValidSortByDirection(string? direction) =>
        direction is null || ValidSortByDirections.Any(d => d.Equals(direction, StringComparison.OrdinalIgnoreCase));

    private static bool BeEmptyOrAValidPhase(string? phase) =>
        phase is null || ValidPhases.Any(p => p.Equals(phase, StringComparison.OrdinalIgnoreCase));
}

public static class PagedSchoolRisksValidatorValues
{
    public static string[] SortField => PagedSchoolRisksParametersValidator.ValidSortByFields;
    public static string[] SortOrder => PagedSchoolRisksParametersValidator.ValidSortByDirections;
    public static string[] Phase => PagedSchoolRisksParametersValidator.ValidPhases;
}
