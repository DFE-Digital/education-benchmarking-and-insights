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

    public IEnumerable<RagSchoolSummaryViewModel> PrioritySchools => RagRatings
        .Where(NotOther)
        .GroupBy(r => r.URN)
        .Select(g => new RagSchoolSummaryViewModel
        {
            Urn = g.Key,
            Name = Schools.FirstOrDefault(s => s.URN == g.Key)?.SchoolName,
            Red = g.Count(r => Red(r.RAG)),
            Amber = g.Count(r => Amber(r.RAG)),
            Green = g.Count(r => Green(r.RAG)),
            TopCategories = RagRatings
                .Where(r => r.URN == g.Key)
                .Where(NotOther)
                .Select(r => new RagCategorySummary
                {
                    Category = r.Category!,
                    Value = r.Value,
                    Unit = Lookups.CategoryUnitMap[r.Category!]
                })
                .OrderByDescending(c => c.Value)
                .Take(2)
                .ToList()
        })
        .OrderByDescending(s => s.Red)
        .ThenByDescending(s => s.Amber)
        .ThenBy(s => s.Name)
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

public class RagSchoolSummaryViewModel
{
    public string? Urn { get; init; }
    public string? Name { get; init; }

    public int Red { get; init; }
    public int Amber { get; init; }
    public int Green { get; init; }

    public IReadOnlyList<RagCategorySummary> TopCategories { get; init; } = [];
}

public class RagCategorySummary
{
    public string Category { get; init; } = string.Empty;
    public decimal? Value { get; init; }
    public string Unit { get; init; } = string.Empty;
}