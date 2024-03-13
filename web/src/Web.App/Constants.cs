using System.Diagnostics.CodeAnalysis;

namespace Web.App;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ServiceName = "Financial Benchmarking and Insights Tool";

    public const string InsightApi = "Insight";
    public const string EstablishmentApi = "Establishment";
    public const string BenchmarkApi = "Benchmark";

    public const string SectionInsightApi = "Apis:Insight";
    public const string SectionEstablishmentApi = "Apis:Establishment";
    public const string SectionBenchmarkApi = "Apis:Benchmark";

    public static string GiasSchoolUrl(string? urn) => $"https://www.get-information-schools.service.gov.uk/establishments/establishment/details/{urn}";
    public static string GiasTrustUrl(string? uid) => $"https://www.get-information-schools.service.gov.uk/groups/group/details/{uid}";

    public static int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    public static IEnumerable<int> AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();
}