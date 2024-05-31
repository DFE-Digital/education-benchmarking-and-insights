using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolPlanViewModel(School school, IEnumerable<FinancialPlan> plans)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;

    public IEnumerable<FinancialPlan> Plans => plans;
}