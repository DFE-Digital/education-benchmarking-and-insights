using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;


public class SchoolPlanViewModel(School school, IEnumerable<FinancialPlan> plans)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;

    public IEnumerable<FinancialPlan> Plans => plans;
}




