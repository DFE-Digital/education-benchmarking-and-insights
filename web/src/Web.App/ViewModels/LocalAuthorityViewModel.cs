using Web.App.Domain;
namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(LocalAuthority localAuthority)
{
    public LocalAuthorityViewModel(LocalAuthority localAuthority, FinanceYears years) : this(localAuthority)
    {
        Section251Year = years.S251;
    }

    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? Section251Year { get; }

    public IEnumerable<IGrouping<string?, LocalAuthoritySchool>> GroupedSchools { get; } = localAuthority.Schools
        .OrderBy(x => x.SchoolName)
        .GroupBy(x => x.OverallPhase)
        .OrderBy(x => GetLaPhaseOrder(x.Key));

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

public class LocalAuthoritySchoolsSectionViewModel
{
    public string? Heading { get; init; }
    public int Id { get; init; }
    public IEnumerable<LocalAuthoritySchool> Schools { get; init; } = [];
}