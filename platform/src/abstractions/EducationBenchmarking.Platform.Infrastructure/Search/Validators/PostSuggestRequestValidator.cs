using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace EducationBenchmarking.Platform.Infrastructure.Search.Validators;

[ExcludeFromCodeCoverage]
public class PostSuggestRequestValidator : AbstractValidator<PostSuggestRequest>
{
    public PostSuggestRequestValidator()
    {
        RuleFor(p => p.SuggesterName).NotNull();
        RuleFor(p => p.SearchText).NotNull();
        RuleFor(p => p.Size).GreaterThanOrEqualTo(5);
    }
}