using System;
using System.Collections.Generic;

namespace Platform.Api.Benchmark.Comparators;

public record ComparatorSchools
{
    public long? TotalSchools { get; set; }
    public IEnumerable<string> Schools { get; set; } = Array.Empty<string>();
}