using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Establishment.Comparators;

public record ComparatorSchoolsRequest
{
    public CharacteristicList? FinanceType { get; set; }
    public CharacteristicList? OverallPhase { get; set; }
    public CharacteristicList? LAName { get; set; }
    public CharacteristicList? SchoolPosition { get; set; }
    public CharacteristicValueBool? IsPFISchool { get; set; }
    public CharacteristicList? LondonWeighting { get; set; }
    public CharacteristicList? OfstedDescription { get; set; }
    public CharacteristicRange? TotalPupils { get; set; }
    public CharacteristicRange? BuildingAverageAge { get; set; }
    public CharacteristicRange? TotalInternalFloorArea { get; set; }
    public CharacteristicRange? PercentFreeSchoolMeals { get; set; }
    public CharacteristicRange? PercentSpecialEducationNeeds { get; set; }
    public CharacteristicRange? TotalPupilsSixthForm { get; set; }
    public CharacteristicRange? KS2Progress { get; set; }
    public CharacteristicRange? KS4Progress { get; set; }
    public CharacteristicRange? SchoolsInTrust { get; set; }
    public CharacteristicRange? PercentWithVI { get; set; }
    public CharacteristicRange? PercentWithSPLD { get; set; }
    public CharacteristicRange? PercentWithSLD { get; set; }
    public CharacteristicRange? PercentWithSLCN { get; set; }
    public CharacteristicRange? PercentWithSEMH { get; set; }
    public CharacteristicRange? PercentWithPMLD { get; set; }
    public CharacteristicRange? PercentWithPD { get; set; }
    public CharacteristicRange? PercentWithOTH { get; set; }
    public CharacteristicRange? PercentWithMSI { get; set; }
    public CharacteristicRange? PercentWithMLD { get; set; }
    public CharacteristicRange? PercentWithHI { get; set; }
    public CharacteristicRange? PercentWithASD { get; set; }

    public string FilterExpression(string urn) => new List<string>()
        .NotValueFilter("URN", urn)
        .RangeFilter(nameof(TotalPupils), TotalPupils)
        .RangeFilter(nameof(BuildingAverageAge), BuildingAverageAge)
        .RangeFilter(nameof(TotalInternalFloorArea), TotalInternalFloorArea)
        .RangeFilter(nameof(TotalPupilsSixthForm), TotalPupilsSixthForm)
        .RangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
        .RangeFilter(nameof(PercentSpecialEducationNeeds), PercentSpecialEducationNeeds)
        .RangeFilter(nameof(KS2Progress), KS2Progress)
        .RangeFilter(nameof(KS4Progress), KS4Progress)
        .RangeFilter(nameof(SchoolsInTrust), SchoolsInTrust)
        .RangeFilter(nameof(IsPFISchool), IsPFISchool)
        .RangeFilter(nameof(PercentWithVI), PercentWithVI)
        .RangeFilter(nameof(PercentWithSPLD), PercentWithSPLD)
        .RangeFilter(nameof(PercentWithSLD), PercentWithSLD)
        .RangeFilter(nameof(PercentWithSLCN), PercentWithSLCN)
        .RangeFilter(nameof(PercentWithSEMH), PercentWithSEMH)
        .RangeFilter(nameof(PercentWithPMLD), PercentWithPMLD)
        .RangeFilter(nameof(PercentWithPD), PercentWithPD)
        .RangeFilter(nameof(PercentWithOTH), PercentWithOTH)
        .RangeFilter(nameof(PercentWithMSI), PercentWithMSI)
        .RangeFilter(nameof(PercentWithMLD), PercentWithMLD)
        .RangeFilter(nameof(PercentWithHI), PercentWithHI)
        .RangeFilter(nameof(PercentWithASD), PercentWithASD)
        .ValuesFilter(nameof(FinanceType), FinanceType)
        .ValuesFilter(nameof(OverallPhase), OverallPhase)
        .ValuesFilter(nameof(LAName), LAName)
        .ValuesFilter(nameof(SchoolPosition), SchoolPosition)
        .ValuesFilter(nameof(LondonWeighting), LondonWeighting)
        .ValuesFilter(nameof(OfstedDescription), OfstedDescription)
        .BuildFilter();

    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public string SearchExpression() => new List<string>()
        .BuildSearch();
}