using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustFinancialBenchmarkingInsightsSummaryViewModel(Trust trust)
{
    public string? Name => trust.TrustName;
    public string? CompanyNumber => trust.CompanyNumber;
}