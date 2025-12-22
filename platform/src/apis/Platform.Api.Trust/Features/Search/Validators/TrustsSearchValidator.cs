using FluentValidation;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search.Validators;

public class TrustsSearchValidator : AbstractValidator<SearchRequest>
{
    public TrustsSearchValidator()
    {
        Include(new PostSearchRequestValidator());

        RuleFor(x => x.OrderBy)
            .Must(orderBy => orderBy == null || orderBy.Field == "TrustNameSortable")
            .WithMessage("OrderBy Field must be TrustNameSortable");
    }
}