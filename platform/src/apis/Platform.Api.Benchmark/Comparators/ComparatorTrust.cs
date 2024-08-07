using System;
using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Benchmark.Comparators;

[ExcludeFromCodeCoverage]
public record ComparatorTrust
{
    public string? CompanyNumber { get; set; }
    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
    public double? TotalIncome { get; set; }
    public string[]? PhasesCovered { get; set; }
    public DateTime? OpenDate { get; set; }
    public double? PercentFreeSchoolMeals { get; set; }
    public double? PercentSpecialEducationNeeds { get; set; }
    public double? TotalInternalFloorArea { get; set; }
}