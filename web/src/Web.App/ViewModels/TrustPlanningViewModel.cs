using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustPlanningViewModel(Trust trust, School[] schools, FinancialPlan[] plans)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public School[] Schools => schools;
    public FinancialPlan[] Plans => plans.Where(x => x.IsComplete).ToArray();
}