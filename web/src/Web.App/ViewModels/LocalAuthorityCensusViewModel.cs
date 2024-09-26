using Web.App.Domain;
namespace Web.App.ViewModels;

public class LocalAuthorityCensusViewModel(LocalAuthority localAuthority)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string[] Phases => localAuthority.Schools
        .GroupBy(x => x.OverallPhase)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Key)
        .OfType<string>()
        .ToArray();
}