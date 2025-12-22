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
    }
}