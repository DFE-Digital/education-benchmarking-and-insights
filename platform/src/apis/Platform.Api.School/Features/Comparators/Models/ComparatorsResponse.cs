using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
public record ComparatorsResponse
{
    public long? TotalSchools { get; init; }
    public IEnumerable<string> Schools { get; init; } = [];
}