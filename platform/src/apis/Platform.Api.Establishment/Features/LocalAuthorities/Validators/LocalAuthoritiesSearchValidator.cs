using FluentValidation;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Validators;

public class LocalAuthoritiesSearchValidator : AbstractValidator<SearchRequest>
{
    public LocalAuthoritiesSearchValidator()
    {
        Include(new PostSearchRequestValidator());

        RuleFor(x => x.OrderBy)
            .Must(orderBy => orderBy == null || orderBy.Field == "LocalAuthorityNameSortable")
            .WithMessage("OrderBy Field must be LocalAuthorityNameSortable");
    }
}