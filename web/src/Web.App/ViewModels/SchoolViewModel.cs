using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolViewModel(
    School school,
    Finances finances,
    IEnumerable<RagRating> ratings)
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
    public IEnumerable<RagRating> Ratings => ratings
        .Where(x => x.Status is "Red" or "Amber")
        .OrderBy(x => x.StatusOrder)
        .ThenByDescending(x => x.Decile)
        .ThenByDescending(x => x.Value)
        .Take(3);
}