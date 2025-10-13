using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(LocalAuthority localAuthority)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? NumberOfSchools => localAuthority.Schools.Length;

    public IEnumerable<IGrouping<string?, LocalAuthoritySchool>> GroupedSchools { get; } = localAuthority.Schools
        .OrderBy(x => x.SchoolName)
        .GroupBy(x => x.OverallPhase)
        .OrderBy(x => GetLaPhaseOrder(x.Key));

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);

    public IEnumerable<RagSchoolViewModel> PrimarySchools => GroupedSchoolRags
        .Where(s => s.OverallPhase == OverallPhaseTypes.Primary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> SecondarySchools => GroupedSchoolRags
        .Where(s => s.OverallPhase == OverallPhaseTypes.Secondary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> Special => GroupedSchoolRags
        .Where(s => s.OverallPhase is OverallPhaseTypes.Special)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AlternativeProvision => GroupedSchoolRags
        .Where(s => s.OverallPhase is OverallPhaseTypes.AlternativeProvision)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AllThroughSchools => GroupedSchoolRags
        .Where(s => s.OverallPhase == OverallPhaseTypes.AllThrough)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> UniversityTechnicalColleges => GroupedSchoolRags
        .Where(s => s.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> PostSixteen => GroupedSchoolRags
        .Where(s => s.OverallPhase == OverallPhaseTypes.PostSixteen)
        .SelectMany(s => s.Schools);

    // todo: build from rag summaries, taking top 5 per
    private IEnumerable<(string? OverallPhase, IEnumerable<RagSchoolViewModel> Schools)> GroupedSchoolRags =>
    [
        new ValueTuple<string?, IEnumerable<RagSchoolViewModel>>(OverallPhaseTypes.Primary, new List<RagSchoolViewModel>
            {
                new("123451", "Stub school 1", 1, 3, 0),
                new("123452", "Stub school 2", 1, 2, 1),
                new("123453", "Stub school 3", 2, 1, 2),
                new("123454", "Stub school 4", 2, 3, 3),
                new("123455", "Stub school 5", 3, 2, 4),
                new("123456", "Stub school 6", 3, 1, 5),
            }.OrderByDescending(o => o.RedRatio)
            .ThenByDescending(o => o.AmberRatio)
            .ThenBy(o => o.Name)
            .Take(5)),
        new ValueTuple<string?, IEnumerable<RagSchoolViewModel>>(OverallPhaseTypes.Secondary, new List<RagSchoolViewModel>
            {
                new("223451", "Stub school 7", 1, 3, 0),
                new("223452", "Stub school 8", 1, 2, 1),
                new("223453", "Stub school 9", 2, 1, 2),
                new("223454", "Stub school 10", 2, 3, 3),
                new("223455", "Stub school 11", 3, 2, 4),
                new("223456", "Stub school 12", 3, 1, 5),
            }.OrderByDescending(o => o.RedRatio)
            .ThenByDescending(o => o.AmberRatio)
            .ThenBy(o => o.Name)
            .Take(5))
    ];

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