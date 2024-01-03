using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Api.Benchmark.Models;

[ExcludeFromCodeCoverage]
public class FreeSchoolMealLookupDataObject
{
    public string Term { get; set; }

    public string OverallPhase { get; set; }
        
    public bool? HasSixthForm { get; set; }

    public decimal FSMMin { get; set; } 
        
    public decimal FSMMax { get; set; }

    public string FSMScale { get; set; }
}