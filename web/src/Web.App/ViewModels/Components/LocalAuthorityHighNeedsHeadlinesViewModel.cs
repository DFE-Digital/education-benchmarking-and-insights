using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.NonFinancial;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHeadlinesViewModel(LocalAuthority<HighNeeds> highNeeds, LocalAuthorityNumberOfPlans plans)
{
    public decimal? TotalPlans => plans.Total;
    public decimal? TotalSpend => highNeeds.Outturn?.Total;
    public decimal? TotalSpendPerPlan => TotalSpend > 0 && TotalPlans > 0 ? TotalSpend / TotalPlans : null;
}