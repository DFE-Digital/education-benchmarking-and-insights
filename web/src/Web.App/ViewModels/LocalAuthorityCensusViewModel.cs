using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class LocalAuthorityCensusViewModel(LocalAuthority localAuthority)
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

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.HighNeeds);
}