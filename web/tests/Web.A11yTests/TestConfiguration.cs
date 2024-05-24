using Microsoft.Extensions.Configuration;
namespace Web.A11yTests;

public static class TestConfiguration
{
    public static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
        .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", false)
#endif
        .Build();

    public static ApiEndpoint Benchmark => Instance.GetSection(nameof(Benchmark)).Get<ApiEndpoint>() ??
                                           throw new InvalidOperationException(
                                               "Benchmark API missing from configuration");

    public static string School => Instance.GetValue<string>("SchoolUrn") ??
                                   throw new InvalidOperationException("School URN missing from configuration");

    public static string Trust => Instance.GetValue<string>("TrustCompanyNo") ??
                                  throw new InvalidOperationException("Trust company number missing from configuration");

    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;

    public static AuthenticationSettings Authentication => Instance.GetSection(nameof(Authentication)).Get<AuthenticationSettings>() ??
                                                           throw new InvalidOperationException(
                                                               "Authentication settings missing from configuration");

    public record ApiEndpoint
    {
        public string? Host { get; init; }
        public string? Key { get; init; }
    }

    public record AuthenticationSettings
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}