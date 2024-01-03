using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Api.Benchmark.Models.Characteristics;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Benchmark.Requests;

[ExcludeFromCodeCoverage]
public class ComparatorSetRequest
{
    public bool IncludeSet { get; set; } = false; 
    public int Size { get; set; }
    public Dictionary<string, CharacteristicValue> Characteristics { get; set; }
    public ProximitySort SortMethod { get; set; }
}