using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "trust-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
        public const string Search = "Search";
        public const string Details = "Details";
        public const string Comparators = "Comparators";
        public const string BudgetForecast = "Budget Forecast";
        public const string Accounts = "Accounts Return";
    }
}