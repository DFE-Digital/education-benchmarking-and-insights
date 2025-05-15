using System.Linq;
using FluentValidation;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.CommercialResources.Validators;

public class CommercialResourcesParametersValidator : AbstractValidator<CommercialResourcesParameters>
{
    public CommercialResourcesParametersValidator()
    {
        RuleFor(x => x.Categories)
            .Must(categories => categories == null || ContainValidCategories(categories))
            .WithMessage($"{{PropertyName}} must be null or only contain the supported values: {string.Join(", ", CostCategories.All)}");
    }

    private static bool ContainValidCategories(string[] categories) => categories.All(CostCategories.IsValid);
}