using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents the result of a request to find similar schools.
/// </summary>
public record ComparatorsResponse
{
    /// <summary>
    /// The total number of schools matching the criteria.
    /// </summary>
    public long? TotalSchools { get; init; }
    /// <summary>
    /// The list of Unique Reference Numbers (URNs) for the schools identified as most similar.
    /// </summary>
    public IEnumerable<string> Schools { get; init; } = [];
}