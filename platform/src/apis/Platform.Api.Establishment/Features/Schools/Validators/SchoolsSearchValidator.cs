using System.Linq;
using FluentValidation;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Domain;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools.Validators;

public class SchoolsSearchValidator : AbstractValidator<SearchRequest>
{
    public SchoolsSearchValidator()
    {
        Include(new PostSearchRequestValidator());

        RuleFor(x => x.Filters)
            .Must(filters => filters == null || filters.All(f => f.Field == nameof(School.OverallPhase)))
            .WithMessage($"Each Filter Field must be {nameof(School.OverallPhase)}");

        RuleForEach(x => x.Filters)
            .Must(f => f.Value != null && OverallPhase.All.Contains(f.Value))
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.OrderBy)
            .Must(orderBy => orderBy == null || orderBy.Field == "SchoolNameSortable")
            .WithMessage("OrderBy Field must be SchoolNameSortable");
    }
}