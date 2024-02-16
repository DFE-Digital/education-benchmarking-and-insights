using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.Responses.Characteristics;

namespace EducationBenchmarking.Platform.Domain.Requests;

[ExcludeFromCodeCoverage]
public record ComparatorSetRequest
{
    public bool IncludeSet { get; set; } = false;
    public int Size { get; set; }
    public Dictionary<string, CharacteristicValue>? Characteristics { get; set; }
    public ProximitySort? SortMethod { get; set; }
}