using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Search.Requests;
namespace Platform.Search.Validators;

[ExcludeFromCodeCoverage]
public class PostSuggestRequestValidator : AbstractValidator<SuggestRequest>
{
    public PostSuggestRequestValidator()
    {
        RuleFor(p => p.SuggesterName).NotNull();
        RuleFor(p => p.SearchText).NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(p => p.Size).GreaterThanOrEqualTo(5);
    }
}