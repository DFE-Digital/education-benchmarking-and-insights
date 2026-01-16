using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolSeniorLeadershipViewModel(School school, SeniorLeadershipGroup[] group)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SeniorLeadershipGroup[] Group => group;
    public string? ChartSvg { get; set; }

    public Views.ViewAsOptions ViewAs { get; set; } = Views.ViewAsOptions.Chart;
    public CensusDimensions.ResultAsOptions ResultAs { get; set; } = CensusDimensions.ResultAsOptions.Total;

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}