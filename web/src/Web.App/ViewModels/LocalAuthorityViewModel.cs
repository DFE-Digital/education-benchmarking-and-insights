using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(LocalAuthority localAuthority, RagRatingSummary[] ragRatings)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? NumberOfSchools => localAuthority.Schools.Length;

    public IEnumerable<IGrouping<string?, LocalAuthoritySchool>> GroupedSchoolNames { get; } = localAuthority.Schools
        .OrderBy(x => x.SchoolName)
        .GroupBy(x => x.OverallPhase)
        .OrderBy(x => GetLaPhaseOrder(x.Key));

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);

    public IEnumerable<RagSchoolViewModel> PrimarySchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.Primary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> SecondarySchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.Secondary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> Special => GroupedSchools
        .Where(s => s.OverallPhase is OverallPhaseTypes.Special)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AlternativeProvision => GroupedSchools
        .Where(s => s.OverallPhase is OverallPhaseTypes.AlternativeProvision)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AllThroughSchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.AllThrough)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> UniversityTechnicalColleges => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> PostSixteen => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.PostSixteen)
        .SelectMany(s => s.Schools);

    private IEnumerable<(string? OverallPhase, IEnumerable<RagSchoolViewModel> Schools)> GroupedSchools => ragRatings
        .GroupBy(x => x.OverallPhase)
        .Select(x => (
            OverallPhase: x.Key,
            Schools: x
                .Select(s => new RagSchoolViewModel(
                    s.URN,
                    s.SchoolName,
                    s.RedCount ?? 0,
                    s.AmberCount ?? 0,
                    s.GreenCount ?? 0
                )).OrderByDescending(o => o.RedRatio)
                .ThenByDescending(o => o.AmberRatio)
                .ThenBy(o => o.Name)
                .Take(5)));

    private static int GetLaPhaseOrder(string? phase)
    {
        return phase switch
        {
            OverallPhaseTypes.Primary => 1,
            OverallPhaseTypes.Secondary => 2,
            OverallPhaseTypes.Special => 3,
            OverallPhaseTypes.PupilReferralUnit => 4,
            OverallPhaseTypes.Nursery => 5,
            OverallPhaseTypes.AllThrough => 6,
            OverallPhaseTypes.PostSixteen => 7,
            _ => 99
        };
    }
}

public class LocalAuthoritySchoolNamesSectionViewModel
{
    public string? Heading { get; init; }
    public int Id { get; init; }
    public IEnumerable<LocalAuthoritySchool> Schools { get; init; } = [];
}

public class LocalAuthoritySchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}