using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class PostSuggestRequestValidator : AbstractValidator<PostSuggestRequestModel>
{
    public PostSuggestRequestValidator()
    {
        RuleFor(p => p.SuggesterName).NotNull();
        RuleFor(p => p.SearchText).NotNull();
        RuleFor(p => p.Size).GreaterThanOrEqualTo(5);
    }
}