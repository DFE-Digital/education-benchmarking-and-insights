using System.Collections.Generic;

namespace Platform.Api.Benchmark.Comparators;

public record ComparatorTrustsRequest
{
    public string? Target { get; set; }
    public CharacteristicList? PhasesCovered { get; set; }
    public CharacteristicRange? TotalPupils { get; set; }
    public CharacteristicRange? TotalIncome { get; set; }
    public CharacteristicDateRange? OpenDate { get; set; }
    public CharacteristicRange? TotalInternalFloorArea { get; set; }
    public CharacteristicRange? PercentFreeSchoolMeals { get; set; }
    public CharacteristicRange? PercentSpecialEducationNeeds { get; set; }
    public CharacteristicRange? SchoolsInTrust { get; set; }

    public string FilterExpression()
    {
        return new List<string>()
            .AddNotValueFilter("CompanyNumber", Target)
            .AddRangeFilter(nameof(TotalPupils), TotalPupils)
            .AddRangeFilter(nameof(TotalIncome), TotalIncome)
            .AddRangeFilter(nameof(TotalInternalFloorArea), TotalInternalFloorArea)
            .AddRangeFilter(nameof(OpenDate), OpenDate)
            .AddRangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
            .AddRangeFilter(nameof(PercentSpecialEducationNeeds), PercentSpecialEducationNeeds)
            .AddRangeFilter(nameof(SchoolsInTrust), SchoolsInTrust)
            .BuildFilter();
    }

    public string SearchExpression()
    {
        return new List<string>()
            .AddListSearch(nameof(PhasesCovered), PhasesCovered)
            .BuildSearch();
    }

};