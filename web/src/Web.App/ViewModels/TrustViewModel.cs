using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustViewModel(Trust trust, IReadOnlyCollection<School> schools, IEnumerable<RagRating> ratings)
{
    private const string Red = "Red";
    private const string Amber = "Amber";
    private const string Green = "Green";

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
    public int NumberSchools => schools.Count;
    public int Low => ratings.Count(x => x.Status == Green);
    public int Medium => ratings.Count(x => x.Status == Amber);
    public int High => ratings.Count(x => x.Status == Red);

    public IOrderedEnumerable<RagCostCategoryViewModel> Ratings => ratings
        .GroupBy(x => (x.Status, x.CostCategory))
        .Select(x => (x.Key.Status, x.Key.CostCategory, Count: x.Count()))
        .GroupBy(x => x.CostCategory)
        .Where(x => x.Key != "Other")
        .Select(x => new RagCostCategoryViewModel(
            x.Key!,
            x.Where(r => r.Status == Red).Select(r => r.Count).SingleOrDefault(),
            x.Where(a => a.Status == Amber).Select(a => a.Count).SingleOrDefault(),
            x.Where(g => g.Status == Green).Select(g => g.Count).SingleOrDefault()
        ))
        .OrderByDescending(x => x.Weighting);

    public IEnumerable<(
        string? OverallPhase,
        IOrderedEnumerable<RagSchoolViewModel> Schools)> Schools => schools
        .GroupBy(x => x.OverallPhase)
        .Select(x => (
            OverallPhase: x.Key,
            Schools: x
                .Select(s => new RagSchoolViewModel(
                    s.Urn,
                    s.Name,
                    ratings.Count(r => r.Urn == s.Urn && r.Status == Red),
                    ratings.Count(r => r.Urn == s.Urn && r.Status == Amber),
                    ratings.Count(r => r.Urn == s.Urn && r.Status == Green)
                ))
                .OrderByDescending(s => s.Weighting)));

    public IEnumerable<RagSchoolViewModel> PrimarySchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.Primary)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> SecondarySchools =>
        Schools.Where(s => s.OverallPhase == OverallPhaseTypes.Secondary)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> SpecialOrPruSchools =>
        Schools.Where(s => s.OverallPhase is OverallPhaseTypes.Special or OverallPhaseTypes.PupilReferralUnit)
            .SelectMany(s => s.Schools);
    public IEnumerable<RagSchoolViewModel> OtherSchools =>
        Schools.Where(s =>
                s.OverallPhase != OverallPhaseTypes.Primary &&
                s.OverallPhase != OverallPhaseTypes.Secondary &&
                s.OverallPhase != OverallPhaseTypes.Special &&
                s.OverallPhase != OverallPhaseTypes.PupilReferralUnit)
            .SelectMany(s => s.Schools);
}

public class TrustSchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}