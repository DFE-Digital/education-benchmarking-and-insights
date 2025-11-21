namespace Web.App.ViewModels.Components;

public class ResourcesViewModel(string identifier, IEnumerable<Resources> resources, string? compareSchoolPerformanceUrl)
{
    public IEnumerable<Resources> Resources => resources;
    public string Identifier => identifier;
    public string? CompareSchoolPerformanceUrl => compareSchoolPerformanceUrl;
}

public enum Resources
{
    SchoolResources,
    SchoolHistoricData,
    TrustResources,
    TrustHistoricData,
    SchoolDetails,
    TrustDetails,
    LocalAuthorityResources,
    DataSource,
    FinancialBenchmarkingInsightsSummary,
    TrustFinancialBenchmarkingInsightsSummary,
    SchoolPerformance
}