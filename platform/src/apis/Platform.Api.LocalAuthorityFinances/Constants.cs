using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthorityFinances;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "local-authority-finances-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
    }
}