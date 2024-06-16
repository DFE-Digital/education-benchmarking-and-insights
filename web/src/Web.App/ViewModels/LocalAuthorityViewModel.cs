using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(LocalAuthority localAuthority)
{
    public LocalAuthorityViewModel(
        LocalAuthority localAuthority,
        IReadOnlyCollection<School> schools)
        : this(localAuthority)
    {
        PrimarySchools = schools.Where(IsPrimary).OrderBy(x => x.SchoolName);
        SecondarySchools = schools.Where(IsSecondary).OrderBy(x => x.SchoolName);
        SpecialOrPruSchools = schools.Where(IsSpecialOrPru).OrderBy(x => x.SchoolName);
        OtherSchools = schools.Where(IsOther).OrderBy(x => x.SchoolName);
    }

    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public IEnumerable<School> PrimarySchools { get; } = [];
    public IEnumerable<School> SecondarySchools { get; } = [];
    public IEnumerable<School> SpecialOrPruSchools { get; } = [];
    public IEnumerable<School> OtherSchools { get; } = [];

    private static bool IsPrimary(School school) => school.OverallPhase is OverallPhaseTypes.Primary;
    private static bool IsSecondary(School school) => school.OverallPhase is OverallPhaseTypes.Secondary;
    private static bool IsSpecialOrPru(School school) => school.OverallPhase is OverallPhaseTypes.Special or OverallPhaseTypes.PupilReferralUnit;
    private static bool IsOther(School school) => !IsPrimary(school) && !IsSecondary(school) && !IsSpecialOrPru(school);
}

public class LocalAuthoritySchoolsSectionViewModel
{
    public string? Heading { get; init; }
    public int Id { get; init; }
    public IEnumerable<School> Schools { get; init; } = [];
}
