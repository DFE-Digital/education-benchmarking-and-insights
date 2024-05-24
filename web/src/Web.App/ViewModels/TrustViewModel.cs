using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustViewModel(Trust trust, Balance balance, IReadOnlyCollection<School> schools, IEnumerable<RagRating> ratings)
{

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
    public int NumberSchools => schools.Count;
    public decimal? RevenueReserve => balance.RevenueReserve;
    public decimal? InYearBalance => balance.InYearBalance;
    public int Low => ratings.Where(NotOther).Count(Green);
    public int Medium => ratings.Where(NotOther).Count(Amber);
    public int High => ratings.Where(NotOther).Count(Red);

    public IEnumerable<RagCostCategoryViewModel> Ratings => ratings
        .Where(NotOther)
        .GroupBy(x => (x.Status, x.CostCategory))
        .Select(x => (x.Key.Status, x.Key.CostCategory, Count: x.Count()))
        .GroupBy(x => x.CostCategory)
        .Select(x => new RagCostCategoryViewModel(
            x.Key!,
            x.Where(w => Red(w.Status)).Select(r => r.Count).SingleOrDefault(),
            x.Where(w => Amber(w.Status)).Select(a => a.Count).SingleOrDefault(),
            x.Where(w => Green(w.Status)).Select(g => g.Count).SingleOrDefault()
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

    private IEnumerable<(
        string? OverallPhase,
        IOrderedEnumerable<RagSchoolViewModel> Schools)> Schools => schools
        .GroupBy(x => x.OverallPhase)
        .Select(x => (
            OverallPhase: x.Key,
            Schools: x
                .Select(s => new RagSchoolViewModel(
                    s.Urn,
                    s.Name,
                    ratings.Where(NotOther).Where(Red).Count(r => r.Urn == s.Urn),
                    ratings.Where(NotOther).Where(Amber).Count(r => r.Urn == s.Urn),
                    ratings.Where(NotOther).Where(Green).Count(r => r.Urn == s.Urn)
                ))
                .OrderByDescending(o => o.RedRatio)
                .ThenByDescending(o => o.AmberRatio)
                .ThenBy(o => o.Name)
            ));

    private static bool NotOther(RagRating ragRating) => ragRating.CostCategory != "Other";
    private static bool Red(RagRating ragRating) => Red(ragRating.Status);
    private static bool Amber(RagRating ragRating) => Amber(ragRating.Status);
    private static bool Green(RagRating ragRating) => Green(ragRating.Status);
    private static bool Red(string? status) => status == "Red";
    private static bool Amber(string? status) => status == "Amber";
    private static bool Green(string? status) => status == "Green";
}

public class TrustSchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}