using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolViewModel(
    School school,
    Finances finances,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public string? OverallPhase => school.OverallPhase;
    public string? OfstedRating => school.OfstedRating;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? TrustIdentifier => school.CompanyNumber;
    public string? TrustName => school.TrustOrCompanyName;
    public decimal InYearBalance => finances.TotalIncome - finances.TotalExpenditure;
    public decimal RevenueReserve => finances.RevenueReserve;

    public Dictionary<string, Dictionary<string, Category>> Categories => CategoryBuilder.Build(pupilExpenditure, areaExpenditure);
}