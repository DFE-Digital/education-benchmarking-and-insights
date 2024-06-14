using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustViewModel(
    Trust trust,
    TrustBalance balance,
    IReadOnlyCollection<School> schools,
    IEnumerable<RagRating> ratings,
    bool? comparatorGenerated)
{

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public int NumberSchools => schools.Count;
    public decimal? RevenueReserve => balance.TotalRevenueReserve;
    public decimal? InYearBalance => balance.TotalInYearBalance;
    public int Low => ratings.Where(NotOther).Count(Green);
    public int Medium => ratings.Where(NotOther).Count(Amber);
    public int High => ratings.Where(NotOther).Count(Red);

    public IEnumerable<RagCostCategoryViewModel> Ratings => ratings
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
        .ThenBy(o => o.Category);

    public IEnumerable<RagSchoolViewModel> NurserySchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.Nursery)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> PrimarySchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.Primary)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> SecondarySchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.Secondary)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> AllThroughSchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.AllThrough)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> SpecialOrPruSchools =>
        Schools.Where(s => s.OverallPhase is OverallPhaseTypes.Special or OverallPhaseTypes.PupilReferralUnit)
            .SelectMany(s => s.Schools);

    public bool? ComparatorGenerated => comparatorGenerated;

    private IEnumerable<(
        string? OverallPhase,
        IOrderedEnumerable<RagSchoolViewModel> Schools)> Schools => schools
        .GroupBy(x => x.OverallPhase)
        .Select(x => (
            OverallPhase: x.Key,
            Schools: x
                .Select(s => new RagSchoolViewModel(
                    s.URN,
                    s.SchoolName,
                    ratings.Where(NotOther).Where(Red).Count(r => r.URN == s.URN),
                    ratings.Where(NotOther).Where(Amber).Count(r => r.URN == s.URN),
                    ratings.Where(NotOther).Where(Green).Count(r => r.URN == s.URN)
                ))
                .OrderByDescending(o => o.RedRatio)
                .ThenByDescending(o => o.AmberRatio)
                .ThenBy(o => o.Name)
            ));

    private static bool NotOther(RagRating ragRating) => ragRating.Category != Category.Other;
    private static bool Red(RagRating ragRating) => Red(ragRating.RAG);
    private static bool Amber(RagRating ragRating) => Amber(ragRating.RAG);
    private static bool Green(RagRating ragRating) => Green(ragRating.RAG);
    private static bool Red(string? status) => status == "red";
    private static bool Amber(string? status) => status == "amber";
    private static bool Green(string? status) => status == "green";
}

public class TrustSchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}