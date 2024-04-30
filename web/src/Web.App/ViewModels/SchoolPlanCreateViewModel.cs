using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolPlanCreateViewModel(School school)
{
    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput) : this(school)
    {
        PlanInput = planInput;
    }

    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput, Finances finances) : this(school, planInput)
    {
        Finances = finances;
    }

    public School School { get; } = school;
    public FinancialPlanInput? PlanInput { get; init; }
    public Finances? Finances { get; init; }
}