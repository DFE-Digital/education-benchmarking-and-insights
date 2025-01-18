using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Platform.Domain;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Api.Establishment.Features.Trusts.Requests;

[ExcludeFromCodeCoverage]
public record TrustComparatorsRequest
{
    public CharacteristicList? PhasesCovered { get; set; }
    public CharacteristicRange? TotalPupils { get; set; }
    public CharacteristicRange? TotalIncome { get; set; }
    public CharacteristicDateRange? OpenDate { get; set; }
    public CharacteristicRange? TotalInternalFloorArea { get; set; }
    public CharacteristicRange? PercentFreeSchoolMeals { get; set; }
    public CharacteristicRange? PercentSpecialEducationNeeds { get; set; }
    public CharacteristicRange? SchoolsInTrust { get; set; }

    public string FilterExpression(string companyNumber) => new List<string>()
        .NotValueFilter("CompanyNumber", companyNumber)
        .RangeFilter(nameof(TotalPupils), TotalPupils)
        .RangeFilter(nameof(TotalIncome), TotalIncome)
        .RangeFilter(nameof(TotalInternalFloorArea), TotalInternalFloorArea)
        .RangeFilter(nameof(OpenDate), OpenDate)
        .RangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
        .RangeFilter(nameof(PercentSpecialEducationNeeds), PercentSpecialEducationNeeds)
        .RangeFilter(nameof(SchoolsInTrust), SchoolsInTrust)
        .BuildFilter();

    public string SearchExpression() => new List<string>()
        .ListSearch(nameof(PhasesCovered), PhasesCovered)
        .BuildSearch();
}