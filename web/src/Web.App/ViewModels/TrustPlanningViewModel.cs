using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustPlanningViewModel(Trust trust, FinancialPlan[] plans)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public TrustSchool[] Schools => trust.Schools;
    public FinancialPlan[] Plans => plans.Where(x => x.IsComplete).ToArray();
}