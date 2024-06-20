using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustForecastViewModel(Trust trust, TrustBalance balance)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    // red
    public bool BalancesInDeficit => balance.InYearBalance < 0;
    public bool BalancesInDeficitOrForecastingDeficit => false;
    public bool SteepDeclineInBalances => false;
    public bool SteepDeclineInBalancesAndHighProportionStaffCosts => false;
    public bool IsRed => BalancesInDeficit
                         || BalancesInDeficitOrForecastingDeficit
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
}