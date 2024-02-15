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

    public static ApiEndpoint Benchmark => Instance.GetSection(nameof(Benchmark)).Get<ApiEndpoint>() ??
                                           throw new InvalidOperationException(
                                               "Benchmark API missing from configuration");

    public static string School => Instance.GetValue<string>("SchoolUrn") ??
                                   throw new InvalidOperationException("School urn missing from configuration");

    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;

    public record ApiEndpoint
    {
        public string? Host { get; init; }
        public string? Key { get; init; }
    }
}