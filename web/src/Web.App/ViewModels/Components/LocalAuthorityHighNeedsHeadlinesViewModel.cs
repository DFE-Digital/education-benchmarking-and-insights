using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.NonFinancial;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHeadlinesViewModel(LocalAuthorityNumberOfPlans plans, LocalAuthority<HighNeeds> highNeeds, string? commentary)
{
    public decimal? TotalPlans => plans.Total;
    public decimal? CarriedForwardBalance => highNeeds.CarriedForwardBalance;
    public string? Commentary => commentary;
}