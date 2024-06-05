using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolViewModel(
    School school,
    Finances? finances,
    IEnumerable<RagRating> ratings,
    bool? comparatorGenerated)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public string? OverallPhase => school.OverallPhase;
    public string? OfstedRating => school.OfstedDescription;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? TrustIdentifier => school.TrustCompanyNumber;
    public string? TrustName => school.TrustName;
    public decimal? InYearBalance => finances?.TotalIncome - finances?.TotalExpenditure;
    public decimal? RevenueReserve => finances?.RevenueReserve;
    public bool HasFinancials => finances != null;
    public bool HasMetricRag => ratings.Any();
    public IEnumerable<RagRating> Ratings => ratings
        .Where(x => x.RAG is "red" or "amber")
        .OrderBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
        .ThenByDescending(x => x.Decile)
        .ThenByDescending(x => x.Value)
        .Take(3);
    public bool? ComparatorGenerated => comparatorGenerated;
}