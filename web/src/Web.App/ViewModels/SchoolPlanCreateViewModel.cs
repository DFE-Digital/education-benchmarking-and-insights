using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolPlanCreateViewModel(School school, string? referrer = null)
{
    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput, string? referrer = null) : this(school, referrer)
    {
        PlanInput = planInput;
    }

    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput, Finances finances, string? referrer = null) : this(school, planInput, referrer)
    {
        Finances = finances;
    }

    public School School { get; } = school;
    public FinancialPlanInput? PlanInput { get; init; }
    public Finances? Finances { get; init; }
    public string? Referrer => referrer;
}