using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanCreateViewModel(School school)
{
    public SchoolPlanCreateViewModel(School school, FinancialPlan plan) : this(school)
    {
        Plan = plan;
    }

    public SchoolPlanCreateViewModel(School school, FinancialPlan plan, Finances finances) : this(school, plan)
    {
        Finances = finances;
    }

    public School School { get; } = school;
    public FinancialPlan? Plan { get; init; }
    public Finances? Finances { get; init; }
}