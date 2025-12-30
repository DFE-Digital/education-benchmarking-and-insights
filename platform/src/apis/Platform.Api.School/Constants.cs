using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "school-api";
    public const string UrnParam = "{urn:regex(^\\d{{6}}$)}";

    public static class Features
    {
        public const string HealthCheck = "Health Check";
        public const string Search = "Search";
        public const string Details = "Details";
        public const string Comparators = "Comparators";
        public const string MetricRagRatings = "Metric RAG Ratings";
        public const string Accounts = "Accounts Return";
        public const string Census = "Census";
    }
}