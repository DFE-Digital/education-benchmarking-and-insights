using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "insight-api";

    public static class Features
    {
        public const string Balance = "Balance";
        public const string BudgetForecast = "Budget Forecast";
        public const string Census = "Census";
        public const string Income = "Income";
        public const string ItSpend = "IT Spend";
        public const string Expenditure = "Expenditure";
        public const string HealthCheck = "Health Check";
        public const string Schools = "Schools";
        public const string Trust = "Trust";
    }
}