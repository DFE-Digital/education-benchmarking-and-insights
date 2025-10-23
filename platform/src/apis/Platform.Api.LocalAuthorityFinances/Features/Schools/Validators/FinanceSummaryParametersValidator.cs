﻿using System.Linq;
using FluentValidation;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Models;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Validators;

public class FinanceSummaryParametersValidator : AbstractValidator<FinanceSummaryParameters>
{
    public FinanceSummaryParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", Dimensions.Finance.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(x => x.IsEmpty() || x.All(BeValidPhase))
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.Limit)
            .Must(MustBeEmptyOrAValidNumber)
            .WithMessage("Limit must be empty or a number between 1 and 100.");

        RuleFor(x => x.NurseryProvision)
            .Must(x => x.IsEmpty() || x.All(BeAValidNurseryProvision))
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", NurseryProvision.All)}");

        RuleFor(x => x.SixthFormProvision)
            .Must(x => x.IsEmpty() || x.All(BeAValidSixthFormProvision))
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", SixthFormProvision.All)}");

        RuleFor(x => x.SpecialClassesProvision)
            .Must(x => x.IsEmpty() || x.All(BeAValidSpecialClassProvision))
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", SpecialClassProvision.All)}");

        RuleFor(x => x.SortField)
            .Must(BeAValidSortField)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceSummarySortFields.All)}");

        RuleFor(x => x.SortOrder)
            .Must(BeAValidSortDirection)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", SortDirection.All)}");


    }

    private static bool MustBeEmptyOrAValidNumber(string? limit) => string.IsNullOrWhiteSpace(limit) || (int.TryParse(limit, out var parsed) && parsed is >= 1 and <= 100);
    private static bool BeAValidDimension(string dimension) => Dimensions.Finance.IsValid(dimension);
    private static bool BeValidPhase(string? overallPhase) => OverallPhase.IsValid(overallPhase);
    private static bool BeAValidNurseryProvision(string nurseryProvision) => NurseryProvision.IsValid(nurseryProvision);
    private static bool BeAValidSixthFormProvision(string sixthFormProvision) => SixthFormProvision.IsValid(sixthFormProvision);
    private static bool BeAValidSpecialClassProvision(string specialClassProvision) => SpecialClassProvision.IsValid(specialClassProvision);
    private static bool BeAValidSortField(string sortField) => FinanceSummarySortFields.IsValid(sortField);
    private static bool BeAValidSortDirection(string sortOrder) => SortDirection.IsValid(sortOrder);
}