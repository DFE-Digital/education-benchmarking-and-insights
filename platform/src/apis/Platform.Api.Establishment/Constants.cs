using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "establishment-api";

    public static class Features
    {
        public const string Trusts = "Trusts";
        public const string Schools = "Schools";
        public const string LocalAuthorities = "Local Authorities";
        public const string HealthCheck = "Health Check";
    }
}

