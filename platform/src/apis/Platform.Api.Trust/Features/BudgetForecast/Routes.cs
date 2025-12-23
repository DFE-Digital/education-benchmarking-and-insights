namespace Platform.Api.Trust.Features.BudgetForecast;

public static class Routes
{
    public const string ForecastRiskSingle = "trusts/{companyNumber:regex(^\\d{{8}}$)}/budget-forecast/forecast-and-risk";
    public const string ForecastRiskMetrics = "trusts/{companyNumber:regex(^\\d{{8}}$)}/budget-forecast/forecast-and-risk/metrics";
    public const string ItSpendingCollection = "trusts/budget-forecast/it-spending";
    public const string ItSpendingForecast = "trusts/{companyNumber:regex(^\\d{{8}}$)}/budget-forecast/it-spending/forecast";
}