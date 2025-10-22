using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Platform.Api.School.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
public record ComparatorsResponse
{
    public long? TotalSchools { get; init; }
    public IEnumerable<string> Schools { get; init; } = [];
}