using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustForecastViewModel(Trust trust, BudgetForecastReturnMetric[] metrics, BudgetForecastReturn[] currentReturns)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    // red
    public bool BalancesInDeficit => currentReturns.Any(r => r.Year == Constants.CurrentYear - 1 && r.Actual < 0);
    public bool BalancesForecastingDeficit => currentReturns.Any(r => r.Year == Constants.CurrentYear && r.Forecast < 0);
    public bool SteepDeclineInBalances => false;
    public bool SteepDeclineInBalancesAndHighProportionStaffCosts => false;
    public bool IsRed => BalancesInDeficit
                         || BalancesForecastingDeficit
                         || SteepDeclineInBalancesAndHighProportionStaffCosts
                         || SteepDeclineInBalances;

    // amber
    public bool DeclineInBalancesButNoForecastDecline => false;
    public bool DeclineInBalancesButAboveForecastHistory => false;
    public bool SteepInclineInBalancesForecastButBelowForecastHistory => false;
    public bool IsAmber => DeclineInBalancesButNoForecastDecline
                           || DeclineInBalancesButAboveForecastHistory
                           || SteepInclineInBalancesForecastButBelowForecastHistory;

    // green
    public bool BalancesStableAndPositive => false;
    public bool BalancesIncreasingSteadily => false;
    public bool BalancesIncreasingSteeply => false;
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
}