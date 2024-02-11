using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Web.A11yTests;

public static class TestConfiguration
{
    public static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
        .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", optional: false)
#endif
        .Build();

    public static ApiEndpoint Benchmark => Instance.GetSection(nameof(Benchmark)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Benchmark));
    public static string School => Instance.GetValue<string>("SchoolUrn") ?? throw new Exception("Test school urn missing");
    public static IEnumerable<string> Impacts => Instance.GetSection("Impacts").Get<string[]>() ?? ["critical",  "serious"];
    public static string ServiceUrl => Instance.GetValue<string>("ServiceUrl") ?? throw new Exception("Service url missing");
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
    
    public record  ApiEndpoint
    {
        public string? Host { get; init; }
        public string? Key { get; init; }
    }
}