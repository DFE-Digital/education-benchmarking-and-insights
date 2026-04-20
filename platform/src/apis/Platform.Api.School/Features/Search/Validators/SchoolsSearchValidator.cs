using System.Linq;
using FluentValidation;
using Platform.Api.School.Features.Search.Models;
using Platform.Domain;
using Platform.Search;

namespace Platform.Api.School.Features.Search.Validators;

public class SchoolsSearchValidator : AbstractValidator<SearchRequest>
{
    public SchoolsSearchValidator()
    {
        Include(new PostSearchRequestValidator());

        RuleFor(x => x.Filters)
            .Must(filters => filters == null || filters.All(f => f.Field == nameof(SchoolSummaryResponse.OverallPhase)))
            .WithMessage($"'{{PropertyName}}' Field must be '{nameof(SchoolSummaryResponse.OverallPhase)}'");

        RuleForEach(x => x.Filters)
            .Must(f => f.Value != null && OverallPhase.All.Contains(f.Value))
            .WithMessage($"'{{PropertyName}}' is not a recognized overall phase. Valid values are: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.OrderBy)
            .Must(orderBy => orderBy == null || orderBy.Field == "SchoolNameSortable")
            .WithMessage("'OrderBy' Field must be 'SchoolNameSortable'");
    }
}