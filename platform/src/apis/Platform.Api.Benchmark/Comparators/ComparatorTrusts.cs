using System;
using System.Collections.Generic;

namespace Platform.Api.Benchmark.Comparators;

public record ComparatorTrusts
{
    public long? TotalTrusts { get; set; }
    public IEnumerable<string> Trusts { get; set; } = Array.Empty<string>();
}