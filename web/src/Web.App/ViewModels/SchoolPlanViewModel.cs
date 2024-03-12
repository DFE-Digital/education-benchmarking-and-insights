using Web.App.Domain;

namespace Web.App.ViewModels
{
    public class SchoolPlanViewModel(School school, IEnumerable<FinancialPlan> plans)
    {
        public string? Name => school.Name;
        public string? Urn => school.Urn;

        public IEnumerable<FinancialPlan> Plans => plans;
    }
}




