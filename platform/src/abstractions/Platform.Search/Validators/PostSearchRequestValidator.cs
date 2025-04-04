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
            .Must(orderBy => orderBy == null || Sort.IsValid(orderBy.Value))
            .WithMessage($"{{PropertyName}} must empty or be one of the supported values: {string.Join(", ", Sort.All)}");
    }
}

public static class Sort
{
    public const string Asc = nameof(Asc);
    public const string Desc = nameof(Desc);

    public static readonly string[] All =
    [
        Asc.ToLower(),
        Desc.ToLower()
    ];

    public static bool IsValid(string? order) => All.Any(a => a.Equals(order, StringComparison.OrdinalIgnoreCase));
}