using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolPlanCreateViewModel(School school, string? referrer = null)
{
    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput, string? referrer = null) : this(school, referrer)
    {
        PlanInput = planInput;
    }

    public SchoolPlanCreateViewModel(School school, FinancialPlanInput planInput, SchoolIncome income, SchoolExpenditure expenditure, Census workforce, string? referrer = null) : this(school, planInput, referrer)
    {
        Income = income;
        Expenditure = expenditure;
        Workforce = workforce;
    }

    public School School { get; } = school;
    public FinancialPlanInput? PlanInput { get; init; }
    public SchoolIncome? Income { get; init; }
    public SchoolExpenditure? Expenditure { get; init; }
    public Census? Workforce { get; init; }
    public string? Referrer => referrer;
}