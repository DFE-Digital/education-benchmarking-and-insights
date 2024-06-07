using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Api.Benchmark.Comparators;

public record ComparatorSchoolsRequest
{
    public string? Target { get; set; }
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

    public string FilterExpression()
    {
        return new List<string>()
            .AddNotValueFilter("URN", Target)
            .AddRangeFilter(nameof(TotalPupils), TotalPupils)
            .AddRangeFilter(nameof(BuildingAverageAge), BuildingAverageAge)
            .AddRangeFilter(nameof(TotalInternalFloorArea), TotalInternalFloorArea)
            .AddRangeFilter(nameof(TotalPupilsSixthForm), TotalPupilsSixthForm)
            .AddRangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
            .AddRangeFilter(nameof(PercentSpecialEducationNeeds), PercentSpecialEducationNeeds)
            .AddRangeFilter(nameof(KS2Progress), KS2Progress)
            .AddRangeFilter(nameof(KS4Progress), KS4Progress)
            .AddRangeFilter(nameof(SchoolsInTrust), SchoolsInTrust)
            .AddRangeFilter(nameof(IsPFISchool), IsPFISchool)
            .AddRangeFilter(nameof(PercentWithVI), PercentWithVI)
            .AddRangeFilter(nameof(PercentWithSPLD), PercentWithSPLD)
            .AddRangeFilter(nameof(PercentWithSLD), PercentWithSLD)
            .AddRangeFilter(nameof(PercentWithSLCN), PercentWithSLCN)
            .AddRangeFilter(nameof(PercentWithSEMH), PercentWithSEMH)
            .AddRangeFilter(nameof(PercentWithPMLD), PercentWithPMLD)
            .AddRangeFilter(nameof(PercentWithPD), PercentWithPD)
            .AddRangeFilter(nameof(PercentWithOTH), PercentWithOTH)
            .AddRangeFilter(nameof(PercentWithMSI), PercentWithMSI)
            .AddRangeFilter(nameof(PercentWithMLD), PercentWithMLD)
            .AddRangeFilter(nameof(PercentWithHI), PercentWithHI)
            .AddRangeFilter(nameof(PercentWithASD), PercentWithASD)
            .BuildFilter();
    }

    public string SearchExpression()
    {
        return new List<string>()
            .AddListSearch(nameof(FinanceType), FinanceType)
            .AddListSearch(nameof(OverallPhase), OverallPhase)
            .AddListSearch(nameof(LAName), LAName)
            .AddListSearch(nameof(SchoolPosition), SchoolPosition)
            .AddListSearch(nameof(LondonWeighting), LondonWeighting)
            .AddListSearch(nameof(OfstedDescription), OfstedDescription)
            .BuildSearch();
    }
}