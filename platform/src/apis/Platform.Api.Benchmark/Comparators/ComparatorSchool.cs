using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Benchmark.Comparators;

[ExcludeFromCodeCoverage]
public record ComparatorSchool
{
    public string? URN { get; set; }
    public string? FinanceType { get; set; }
    public string? OverallPhase { get; set; }
    public string? LAName { get; set; }
    public double? TotalPupils { get; set; }
    public double? PercentFreeSchoolMeals { get; set; }
    public double? PercentSpecialEducationNeeds { get; set; }
    public string? LondonWeighting { get; set; }
    public double? BuildingAverageAge { get; set; }
    public double? TotalInternalFloorArea { get; set; }
    public string? OfstedDescription { get; set; }
    public int? SchoolsInTrust { get; set; }
    public bool? IsPFISchool { get; set; }
    public double? TotalPupilsSixthForm { get; set; }
    public double? KS2Progress { get; set; }
    public double? KS4Progress { get; set; }
    public double? PercentWithVI { get; set; }
    public double? PercentWithSPLD { get; set; }
    public double? PercentWithSLD { get; set; }
    public double? PercentWithSLCN { get; set; }
    public double? PercentWithSEMH { get; set; }
    public double? PercentWithPMLD { get; set; }
    public double? PercentWithPD { get; set; }
    public double? PercentWithOTH { get; set; }
    public double? PercentWithMSI { get; set; }
    public double? PercentWithMLD { get; set; }
    public double? PercentWithHI { get; set; }
    public double? PercentWithASD { get; set; }
    public string? SchoolPosition { get; set; }
}