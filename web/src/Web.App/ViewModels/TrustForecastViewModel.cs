using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustForecastViewModel(
    Trust trust,
    BudgetForecastReturnMetric[] metrics,
    BudgetForecastReturn[] currentReturns,
    int? bfrYear)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    // Figure for the current financial year end (Y1P2) revenue reserves balances is negative
    public bool BalancesInDeficit => currentReturns.Any(r => r.Year == bfrYear && r.Actual < 0);

    // Figure for the next financial year end (Y2P2) revenue reserves balances is negative.
    public bool BalancesForecastingDeficit => currentReturns.Any(r => r.Year == bfrYear + 1 && r.Forecast < 0);

    // 1) The slope of the graph determined using slope analysis is negative and among the top 10% of negative slopes (the most negative).
    // 2) The most frequent status of volatility analysis for previous years is not “AR above forecast”.
    // 3) Staff costs as percentage of total income is less than 80%
    public bool SteepDeclineInBalances => SlopeAnalysisNegativeAndAmongTheMostNegative
                                          && MostFrequentStatusOfVolatilityAnalyses.All(b => b.VarianceStatus != BudgetForecastVarianceStatusType.ArAboveForecast)
                                          && metrics.Any(m => m.MetricType.IsStaffCosts() && m.Value < 80);

    // 1) The slope of the graph determined using slope analysis is negative and among the top 10% of negative slopes (the most negative).
    // 2) The most frequent status of volatility analysis for previous years is not “AR above forecast”.
    // 3) Staff costs as percentage of total income is more or equal to 80%
    public bool SteepDeclineInBalancesAndHighProportionStaffCosts => SlopeAnalysisNegativeAndAmongTheMostNegative
                                                                     && MostFrequentStatusOfVolatilityAnalyses.All(b => b.VarianceStatus != BudgetForecastVarianceStatusType.ArAboveForecast)
                                                                     && metrics.Any(m => m.MetricType.IsStaffCosts() && m.Value >= 80);
    public bool IsRed => BalancesInDeficit
                         || BalancesForecastingDeficit
                         || SteepDeclineInBalancesAndHighProportionStaffCosts
                         || SteepDeclineInBalances;

    // The slope of the graph determined using slope analysis is negative and not among the top 10% of negative slopes (the most negative).
    public bool DeclineInBalancesButNoForecastDecline => SlopeAnalysisNegativeAndNotAmongTheMostNegative;

    // 1) The slope of the graph determined using slope analysis is negative and among the top 10% of negative slopes (the most negative).
    // 2) The most frequent status of volatility analysis for previous years is “AR above forecast”.
    public bool DeclineInBalancesButAboveForecastHistory => SlopeAnalysisNegativeAndAmongTheMostNegative
                                                            && MostFrequentStatusOfVolatilityAnalyses.Any(b => b.VarianceStatus == BudgetForecastVarianceStatusType.ArAboveForecast);

    // 1) The slope of the graph determined using slope analysis is positive and is among the top 10% of positive slopes
    // 2) The most frequent status of volatility analysis for previous years is “AR significantly below forecast”.
    public bool SteepInclineInBalancesForecastButBelowForecastHistory => SlopeAnalysisPositiveAndAmongTheMostPositive
                                                                         && MostFrequentStatusOfVolatilityAnalyses.Any(b => b.VarianceStatus == BudgetForecastVarianceStatusType.ArSignificantlyBelowForecast);

    public bool IsAmber => DeclineInBalancesButNoForecastDecline
                           || DeclineInBalancesButAboveForecastHistory
                           || SteepInclineInBalancesForecastButBelowForecastHistory;

    // Trust has positive balances, and year-on-year change is less or equal to 10% 
    public bool BalancesStableAndPositive => !BalancesForecastingDeficit
                                             && currentReturns.Any(r => r.Year == bfrYear - 1 && r.Actual > 0)
                                             && currentReturns.Any(r => r.Year == bfrYear && r.Actual > 0)
                                             && (currentReturns.Where(r => r.Year == bfrYear).Select(r => r.Actual.GetValueOrDefault()).FirstOrDefault() -
                                                 currentReturns.Where(r => r.Year == bfrYear - 1).Select(r => r.Actual.GetValueOrDefault()).FirstOrDefault())
                                             / currentReturns.Where(r => r.Year == bfrYear - 1).Select(r => r.Actual.GetValueOrDefault()).FirstOrDefault() <= 0.1m;

    // The slope of the graph determined in slope analysis is positive and not among the top 10% of positive slopes.
    public bool BalancesIncreasingSteadily => SlopeAnalysisPositiveAndNotAmongTheMostPositive;

    // 1) The slope of the graph determined in slope analysis is positive and is among the top 10% of positive slopes.
    // 2) The most frequent status of volatility analysis for previous years is not “AR significantly below forecast”.
    public bool BalancesIncreasingSteeply => SlopeAnalysisPositiveAndAmongTheMostPositive
                                             && MostFrequentStatusOfVolatilityAnalyses.All(b => b.VarianceStatus != BudgetForecastVarianceStatusType.ArSignificantlyBelowForecast);

    public bool IsGreen => BalancesStableAndPositive
                           || BalancesIncreasingSteadily
                           || BalancesIncreasingSteeply;

    public bool HasGuidance => IsRed || IsAmber || IsGreen;

    public int? MetricsYear => metrics
        .Select(x => x.Year)
        .OrderDescending()
        .FirstOrDefault();

    public BudgetForecastReturnMetric[] Metrics => metrics
        .Where(m => m.Year == MetricsYear)
        .ToArray();

    public bool HasMetrics => MetricsYear != null;

    private IOrderedEnumerable<(string VarianceStatus, BudgetForecastReturn[] Statuses, int Count)> OrderedVarianceStatusFrequency => currentReturns
        .GroupBy(r => r.VarianceStatus)
        .Select(g => (VarianceStatus: g.Key!, Statuses: g.ToArray(), Count: g.Count()))
        .OrderByDescending(x => x.Count);

    private IEnumerable<BudgetForecastReturn> MostFrequentStatusOfVolatilityAnalyses =>
        OrderedVarianceStatusFrequency.GroupBy(x => x.Count)
            .Select(x => x.Key)
            .Take(1)
            .Join(OrderedVarianceStatusFrequency, x => x, y => y.Count, (_, s) => s.Statuses)
            .SelectMany(x => x);

    private bool SlopeAnalysisNegativeAndAmongTheMostNegative => metrics.Any(m => m.MetricType.IsSlope() && m.Value < 0)
                                                                 && metrics.Any(m => m.MetricType.IsSlopeFlag() && m.Value == -1);

    private bool SlopeAnalysisNegativeAndNotAmongTheMostNegative => metrics.Any(m => m.MetricType.IsSlope() && m.Value < 0)
                                                                 && metrics.Any(m => m.MetricType.IsSlopeFlag() && m.Value != -1);

    private bool SlopeAnalysisPositiveAndAmongTheMostPositive => metrics.Any(m => m.MetricType.IsSlope() && m.Value > 0)
                                                                 && metrics.Any(m => m.MetricType.IsSlopeFlag() && m.Value == 1);

    private bool SlopeAnalysisPositiveAndNotAmongTheMostPositive => metrics.Any(m => m.MetricType.IsSlope() && m.Value > 0)
                                                                    && metrics.Any(m => m.MetricType.IsSlopeFlag() && m.Value != 1);
}