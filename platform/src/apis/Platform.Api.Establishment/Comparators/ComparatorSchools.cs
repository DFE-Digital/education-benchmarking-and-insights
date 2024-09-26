using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Establishment.Comparators;

[ExcludeFromCodeCoverage]
public record ComparatorSchools
{
    public long? TotalSchools { get; set; }
    public IEnumerable<string> Schools { get; set; } = Array.Empty<string>();
}