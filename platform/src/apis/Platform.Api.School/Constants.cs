using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "school-api";
    public const string UrnParam = "{urn:regex(^\\d{{6}}$)}";

    public static class Features
    {
        /// <summary>
        /// Feature for health check endpoints.
        /// </summary>
        public const string HealthCheck = "Health Check";
        /// <summary>
        /// Feature for searching establishments.
        /// </summary>
        public const string Search = "Search";
        /// <summary>
        /// Feature for establishment details.
        /// </summary>
        public const string Details = "Details";
        /// <summary>
        /// Feature for identifying similar schools for benchmarking.
        /// </summary>
        public const string Comparators = "Comparators";
        /// <summary>
        /// Feature for Red-Amber-Green (RAG) ratings of benchmarking metrics.
        /// </summary>
        public const string MetricRagRatings = "Metric RAG Ratings";
        /// <summary>
        /// Feature for school and trust financial accounts.
        /// </summary>
        public const string Accounts = "Accounts Return";
        /// <summary>
        /// Feature for school census and workforce data.
        /// </summary>
        public const string Census = "Census";
    }
}