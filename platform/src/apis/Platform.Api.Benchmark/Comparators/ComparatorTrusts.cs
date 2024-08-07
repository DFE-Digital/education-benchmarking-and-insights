using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Benchmark.Comparators;

[ExcludeFromCodeCoverage]
public record ComparatorTrusts
{
    public long? TotalTrusts { get; set; }
    public IEnumerable<string> Trusts { get; set; } = Array.Empty<string>();
}