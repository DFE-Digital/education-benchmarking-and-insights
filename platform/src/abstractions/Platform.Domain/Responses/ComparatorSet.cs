using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record ComparatorSet
{
    public int TotalResults { get; set; }
    public IEnumerable<string>? Results { get; set; }

    public static ComparatorSet Create(IEnumerable<string> results)
    {
        var enumerable = results as string[] ?? results.ToArray();

        return new ComparatorSet
        {
            Results = enumerable,
            TotalResults = enumerable.Length
        };
    }
}