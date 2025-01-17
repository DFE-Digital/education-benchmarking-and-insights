using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "insight-api";

    public static class Features
    {
        public const string Balance = "Balance";
        public const string Census = "Census";

        public const string HealthCheck = "Health Check";
    }
}