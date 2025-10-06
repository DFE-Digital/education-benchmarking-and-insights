using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustFinancialBenchmarkingInsightsSummaryViewModel(
    Trust trust,
    TrustBalance? balance,
    IEnumerable<RagRating>? ratings) : ITrustKeyInformationViewModel
{
    public string? Name => trust.TrustName;
    public string? CompanyNumber => trust.CompanyNumber;

    public IEnumerable<TrustSchool> Schools => trust.Schools;

    public IEnumerable<RagCostCategoryViewModel> SpendingPriorities => RagRatings
        .Where(NotOther)
        .GroupBy(x => (x.RAG, x.Category))
        .Select(x => (x.Key.RAG, x.Key.Category, Count: x.Count()))
        .GroupBy(x => x.Category)
        .Select(x => new RagCostCategoryViewModel(
            x.Key,
            x.Where(w => Red(w.RAG)).Select(r => r.Count).SingleOrDefault(),
            x.Where(w => Amber(w.RAG)).Select(a => a.Count).SingleOrDefault(),
            x.Where(w => Green(w.RAG)).Select(g => g.Count).SingleOrDefault()
        ))
        .OrderByDescending(o => o.RedRatio)
        .ThenByDescending(o => o.AmberRatio)
        .ThenBy(o => o.Category)
        .Where(o => o.Red > 0 || o.Amber > 0)
        .Take(2);

    public RagRating[] RagRatings => ratings?.ToArray() ?? [];

    public decimal? InYearBalance => balance?.InYearBalance;
    public decimal? RevenueReserve => balance?.RevenueReserve;
    public int NumberSchools => Schools.Count();


    private static bool NotOther(RagRating ragRating) => ragRating.Category != Category.Other;
    private static bool Red(string? status) => status == "red";
    private static bool Amber(string? status) => status == "amber";
    private static bool Green(string? status) => status == "green";
}