using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace Platform.Api.Insight.Schools;

[ExcludeFromCodeCoverage]
public record SchoolCharacteristic
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressPostcode { get; set; }
    public string? FinanceType { get; set; }
    public string? OverallPhase { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? PercentFreeSchoolMeals { get; set; }
    public decimal? PercentSpecialEducationNeeds { get; set; }
    public string? LondonWeighting { get; set; }
    public decimal? BuildingAverageAge { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }
    public string? OfstedDescription { get; set; }
    public int? SchoolsInTrust { get; set; }
    public bool? IsPFISchool { get; set; }
    public decimal? TotalPupilsSixthForm { get; set; }
    public decimal? KS2Progress { get; set; }
    public decimal? KS4Progress { get; set; }
    public decimal? PercentWithVI { get; set; }
    public decimal? PercentWithSPLD { get; set; }
    public decimal? PercentWithSLD { get; set; }
    public decimal? PercentWithSLCN { get; set; }
    public decimal? PercentWithSEMH { get; set; }
    public decimal? PercentWithPMLD { get; set; }
    public decimal? PercentWithPD { get; set; }
    public decimal? PercentWithOTH { get; set; }
    public decimal? PercentWithMSI { get; set; }
    public decimal? PercentWithMLD { get; set; }
    public decimal? PercentWithHI { get; set; }
    public decimal? PercentWithASD { get; set; }

    public string? SchoolPosition { get; set; }

    public string Address => string.Join(", ", new List<string?>
    {
        AddressTown,
        AddressPostcode
    }.Where(x => !string.IsNullOrEmpty(x)));
}