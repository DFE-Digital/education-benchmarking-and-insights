using Web.App.Domain;
namespace Web.App.ViewModels;

public class LocalAuthorityComparisonViewModel(LocalAuthority localAuthority)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int NumberOfSchools => localAuthority.Schools.Length;
    public string[] Phases => localAuthority.Schools
        .GroupBy(x => x.OverallPhase)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Key)
        .OfType<string>()
        .ToArray();
}