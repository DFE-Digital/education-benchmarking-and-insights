using System.Collections.Generic;
using System.Linq;

namespace Platform.Domain;

public record PostSchoolComparatorsRequestModel
{
    public string? Target { get; set; }
    public CharacteristicList? Sector { get; set; }
    public CharacteristicList? Phase { get; set; }
    public CharacteristicList? LocalAuthority { get; set; }
    public CharacteristicList? SchoolPosition { get; set; }
    public CharacteristicList? PrivateFinanceInitiative { get; set; }
    public CharacteristicList? SchoolType { get; set; }
    public CharacteristicList? Region { get; set; }
    public CharacteristicList? LondonWeighting { get; set; }
    public CharacteristicList? OfstedRating { get; set; }

    public CharacteristicRange? NumberOfPupils { get; set; }
    public CharacteristicRange? AverageBuildingAge { get; set; }
    public CharacteristicRange? GrossInternalFloorArea { get; set; }
    public CharacteristicRange? PercentFreeSchoolMeals { get; set; }
    public CharacteristicRange? PercentSenWithoutPlan { get; set; }
    public CharacteristicRange? PercentSenWithPlan { get; set; }
    public CharacteristicRange? NumberOfPupilsSixthForm { get; set; }
    public CharacteristicRange? KeyStage2Progress { get; set; }
    public CharacteristicRange? KeyStage4Progress { get; set; }
    public CharacteristicRange? NumberSchoolsInTrust { get; set; }

    public string FilterExpression()
    {
        return new List<string>()
            .AddNotValueFilter("URN", Target)
            .AddRangeFilter(nameof(NumberOfPupils), NumberOfPupils)
            .AddRangeFilter(nameof(AverageBuildingAge), AverageBuildingAge)
            .AddRangeFilter(nameof(GrossInternalFloorArea), GrossInternalFloorArea)
            .AddRangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
            .AddRangeFilter(nameof(PercentSenWithoutPlan), PercentSenWithoutPlan)
            .AddRangeFilter(nameof(PercentSenWithPlan), PercentSenWithPlan)
            .AddRangeFilter(nameof(NumberOfPupilsSixthForm), NumberOfPupilsSixthForm)
            .AddRangeFilter(nameof(KeyStage2Progress), KeyStage2Progress)
            .AddRangeFilter(nameof(KeyStage4Progress), KeyStage4Progress)
            .AddRangeFilter(nameof(NumberSchoolsInTrust), NumberSchoolsInTrust)
            .BuildFilter();
    }

    public string SearchExpression()
    {
        return new List<string>()
            .AddListSearch(nameof(Sector), Sector)
            .AddListSearch(nameof(Phase), Phase)
            .AddListSearch(nameof(LocalAuthority), LocalAuthority)
            .AddListSearch(nameof(SchoolPosition), SchoolPosition)
            .AddListSearch(nameof(PrivateFinanceInitiative), PrivateFinanceInitiative)
            .AddListSearch(nameof(SchoolType), SchoolType)
            .AddListSearch(nameof(Region), Region)
            .AddListSearch(nameof(LondonWeighting), LondonWeighting)
            .AddListSearch(nameof(OfstedRating), OfstedRating)
            .BuildSearch();
    }
}

public record CharacteristicList
{
    public string[] Values { get; set; } = [];
}

public record CharacteristicRange
{
    public decimal From { get; set; }
    public decimal To { get; set; }
}

public static class ExpressionBuilder
{
    public static List<string> AddNotValueFilter(this List<string> list, string fieldName, string? value)
    {
        if (value != null)
        {
            list.Add($"({fieldName} ne '{value}')");
        }

        return list;
    }

    public static List<string> AddRangeFilter(this List<string> list, string fieldName, CharacteristicRange? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"(({fieldName} ge {characteristic.From}) and ({fieldName} le {characteristic.To}))");
        }

        return list;
    }

    public static string BuildFilter(this List<string> list)
    {
        return string.Join(" and ", list);
    }

    public static List<string> AddListSearch(this List<string> list, string fieldName, CharacteristicList? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"{fieldName}:({string.Join(") OR ( ", characteristic.Values.Select(a => $"'{a}'"))})");
        }

        return list;
    }

    public static string BuildSearch(this List<string> list)
    {
        return string.Join(" AND ", list);
    }
}