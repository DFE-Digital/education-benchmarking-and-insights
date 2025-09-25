using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class TrustCensusViewModel(Trust trust)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public int NumberOfSchools => trust.Schools.Length;

    public string[] Phases => trust.Schools
        .GroupBy(x => x.OverallPhase)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Key)
        .OfType<string>()
        .ToArray();

    public FinanceToolsViewModel Tools => new(
        trust.CompanyNumber,
        FinanceTools.CompareYourCosts,
        FinanceTools.FinancialPlanning,
        FinanceTools.CentralServices,
        FinanceTools.SpendingComparisonIt,
        FinanceTools.ForecastRisk);
}