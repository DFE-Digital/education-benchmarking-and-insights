using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Domain.DataObjects;

[ExcludeFromCodeCoverage]
public record FreeSchoolMealLookupDataObject
{
    public string? Term { get; set; }

    public string? OverallPhase { get; set; }
        
    public bool? HasSixthForm { get; set; }

    [JsonProperty("FSMMin")] public decimal FsmMin { get; set; } 
    [JsonProperty("FSMMax")] public decimal FsmMax { get; set; }
    [JsonProperty("FSMScale")] public string? FsmScale { get; set; }
}