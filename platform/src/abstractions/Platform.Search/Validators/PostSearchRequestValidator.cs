using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Platform.Search;

[ExcludeFromCodeCoverage]
public class PostSearchRequestValidator : AbstractValidator<SearchRequest>
{
    public PostSearchRequestValidator()
    {
        RuleFor(p => p.SearchText).NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.OrderBy)
            .Must(orderBy => orderBy == null || orderBy.Value == "asc" || orderBy.Value == "desc")
            .WithMessage("OrderBy Value must be 'asc' or 'desc'");
    }
}