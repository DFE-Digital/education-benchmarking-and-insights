using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustForecastMetricsViewModel(int? year, BudgetForecastReturnMetric[] metrics)
{
    public int? Year => year;
    public BudgetForecastReturnMetric[] Metrics => metrics
        .Where(m => !m.MetricType.IsSlope())
        .Where(m => !m.MetricType.IsSlopeFlag())
        .Select(m => m)
        .ToArray();
}