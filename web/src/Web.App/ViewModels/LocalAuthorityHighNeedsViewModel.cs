using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsViewModel(LocalAuthority localAuthority, FinanceYears years)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? Section251Year => years.S251;

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}