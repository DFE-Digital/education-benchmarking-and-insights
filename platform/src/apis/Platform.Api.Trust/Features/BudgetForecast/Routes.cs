namespace Platform.Api.Trust.Features.BudgetForecast;

public static class Routes
{
    public const string ForecastRiskSingle = $"trusts/{Constants.CompanyNumberParam}/budget-forecast/forecast-and-risk";
    public const string ForecastRiskMetrics = $"trusts/{Constants.CompanyNumberParam}/budget-forecast/forecast-and-risk/metrics";
    public const string ItSpendingCollection = "trusts/budget-forecast/it-spending";
    public const string ItSpendingForecast = $"trusts/{Constants.CompanyNumberParam}/budget-forecast/it-spending/forecast";
}