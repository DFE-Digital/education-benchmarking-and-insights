using Microsoft.Extensions.Configuration;

// ReSharper disable NotNullOrRequiredMemberIsNotInitialized

namespace Platform.ApiTests;

public static class TestConfiguration
{
    private static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
            .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", false)
#endif
        .Build();

    public static ApiEndpoint Insight => Instance.GetSection(nameof(Insight)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Insight));
    public static ApiEndpoint Benchmark => Instance.GetSection(nameof(Benchmark)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Benchmark));
    public static ApiEndpoint Establishment => Instance.GetSection(nameof(Establishment)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Establishment));
    public static ApiEndpoint LocalAuthorityFinances => Instance.GetSection(nameof(LocalAuthorityFinances)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(LocalAuthorityFinances));
    public static ApiEndpoint NonFinancial => Instance.GetSection(nameof(NonFinancial)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(NonFinancial));
    public static ApiEndpoint ChartRendering => Instance.GetSection(nameof(ChartRendering)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(ChartRendering));
    public static ApiEndpoint Content => Instance.GetSection(nameof(Content)).Get<ApiEndpoint>() ?? throw new ArgumentNullException(nameof(Content));


#nullable disable warnings
    public record ApiEndpoint
    {
        public string Host { get; init; }
        public string Key { get; init; }
    }
#nullable restore warnings

}