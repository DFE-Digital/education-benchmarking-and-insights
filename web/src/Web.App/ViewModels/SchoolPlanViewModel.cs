using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolPlanViewModel(School school, IEnumerable<FinancialPlan> plans)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;

    public IEnumerable<FinancialPlan> Plans => plans.OrderByDescending(x => x.Year);

    public FinancialPlan? LatestPlan => Plans.FirstOrDefault(x => x.IsComplete);
    public FinancialPlan? PreviousPlan => Plans.Where(x => x.IsComplete).ElementAtOrDefault(1);

    public static string HeadlineClass(string? ragText)
    {
        switch (ragText)
        {
            case "red":
                return "app-headline-high";
            case "amber":
                return "app-headline-medium";
            case "green":
                return "app-headline-low";
            default:
                return string.Empty;
        }
    }

    public static string Different(decimal? current, decimal? previous)
    {
        if (current > previous)
        {
            return "\u25b2";
        }

        if (current < previous)
        {
            return "\u25bc";
        }

        return string.Empty;

    }

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}