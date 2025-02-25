using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.NonFinancial;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "non-financial-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
    }
}