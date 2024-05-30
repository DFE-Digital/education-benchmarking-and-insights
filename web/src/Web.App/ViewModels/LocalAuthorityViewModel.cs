using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(LocalAuthority localAuthority, IReadOnlyCollection<School> schools)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public IEnumerable<School> PrimarySchools => schools.Where(IsPrimary).OrderBy(x => x.SchoolName);
    public IEnumerable<School> SecondarySchools => schools.Where(IsSecondary).OrderBy(x => x.SchoolName);
    public IEnumerable<School> SpecialOrPruSchools => schools.Where(IsSpecialOrPru).OrderBy(x => x.SchoolName);
    public IEnumerable<School> OtherSchools => schools.Where(IsOther).OrderBy(x => x.SchoolName);

    private static bool IsPrimary(School school)
    {
        return school.OverallPhase == OverallPhaseTypes.Primary;
    }

    private static bool IsSecondary(School school)
    {
        return school.OverallPhase == OverallPhaseTypes.Secondary;
    }

    private static bool IsSpecialOrPru(School school)
    {
        return school.OverallPhase == OverallPhaseTypes.Special || school.OverallPhase == OverallPhaseTypes.PupilReferralUnit;
    }

    private static bool IsOther(School school)
    {
        return !IsPrimary(school) && !IsSecondary(school) && !IsSpecialOrPru(school);
    }
}

public class LocalAuthoritySchoolsSectionViewModel
{
    public string? Heading { get; set; }
    public int Id { get; set; }
    public IEnumerable<School> Schools { get; set; } = Enumerable.Empty<School>();
}
