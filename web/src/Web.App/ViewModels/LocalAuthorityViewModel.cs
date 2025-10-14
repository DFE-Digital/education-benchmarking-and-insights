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

    public IEnumerable<(string? OverallPhase, IEnumerable<RagSchoolViewModel> Schools)> GroupedSchools => ragRatings
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
                .Take(5)))
        .OrderBy(x => GetLaPhaseOrder(x.OverallPhase));

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);

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