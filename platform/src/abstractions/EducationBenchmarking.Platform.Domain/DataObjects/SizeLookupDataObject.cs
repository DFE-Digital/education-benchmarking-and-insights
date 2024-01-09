using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.DataObjects;

[ExcludeFromCodeCoverage]
public class SizeLookupDataObject
{
    public string Term { get; set; }
    public string OverallPhase { get; set; }
    public bool? HasSixthForm { get; set; }
    public decimal NoPupilsMin { get; set; }
    public decimal? NoPupilsMax { get; set; }
    public string SizeType { get; set; }
}