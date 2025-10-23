using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthorityFinances;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "local-authority-finances-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
        public const string HighNeeds = "High Needs";
        public const string Schools = "Schools";
    }
}