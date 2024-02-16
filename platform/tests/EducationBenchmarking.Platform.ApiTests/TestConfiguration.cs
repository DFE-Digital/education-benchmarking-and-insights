using Microsoft.Extensions.Configuration;
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized

namespace EducationBenchmarking.Platform.ApiTests;

public static class TestConfiguration
{
    private static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
            .AddJsonFile("appsettings.json", optional: false)
#else
            .AddJsonFile("appsettings.local.json", optional: false)
#endif
            .Build();

    public static ApiEndpoint Insight => Instance.GetSection(nameof(Insight)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Insight));
    public static ApiEndpoint Benchmark => Instance.GetSection(nameof(Benchmark)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Benchmark));
    public static ApiEndpoint Establishment => Instance.GetSection(nameof(Establishment)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Establishment));


#nullable disable warnings
    public record ApiEndpoint
    {
        public string Host { get; init; }
        public string Key { get; init; }
    }
#nullable restore warnings

}