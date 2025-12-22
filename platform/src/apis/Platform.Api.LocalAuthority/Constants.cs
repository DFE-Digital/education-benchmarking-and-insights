using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "local-authority-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
        public const string Search = "Search";
    }
}