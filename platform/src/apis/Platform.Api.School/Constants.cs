using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "school-api";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
    }
}