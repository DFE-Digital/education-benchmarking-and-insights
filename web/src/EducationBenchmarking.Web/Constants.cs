using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ServiceName = "Education benchmarking and insights";
    public const string CorrelationIdHeader = "X-Correlation-ID";
    
    public const string SchoolOrganisationType = "school";
    public const string TrustOrganisationType = "trust";
    
    public const string InsightApi = "Insight";
    public const string EstablishmentApi = "Establishment";
    public const string BenchmarkApi = "Benchmark";
    
    public const string SectionInsightApi = "Apis:Insight";
    public const string SectionEstablishmentApi = "Apis:Establishment";
    public const string SectionBenchmarkApi = "Apis:Benchmark";

    public static string GiasSchoolUrl(string urn) => $"https://www.get-information-schools.service.gov.uk/Establishments/Establishment/Details/{urn}";
    public static string GiasTrustUrl(string uid) => $"https://www.get-information-schools.service.gov.uk/Groups/Group/Details/{uid}";
}