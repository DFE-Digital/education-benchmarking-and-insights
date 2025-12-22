using FluentValidation;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Validators;

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