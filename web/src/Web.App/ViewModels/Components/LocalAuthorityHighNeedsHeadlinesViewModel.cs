using Web.App.Domain.NonFinancial;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHeadlinesViewModel(LocalAuthorityNumberOfPlans plans, string? commentary)
{
    public decimal? TotalPlans => plans.Total;
    public string? Commentary => commentary;
}