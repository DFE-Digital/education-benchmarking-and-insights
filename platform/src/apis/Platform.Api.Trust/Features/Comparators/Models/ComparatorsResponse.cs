using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
public record ComparatorsResponse
{
    public long? TotalTrusts { get; init; }
    public IEnumerable<string> Trusts { get; init; } = [];
}