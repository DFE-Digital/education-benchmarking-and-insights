using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace Web.E2ETests;

public static class TestConfiguration
{
    private static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
        .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", false)
#endif
        .Build();

    public static string ServiceUrl => Instance.GetValue<string>("ServiceUrl") ?? throw new Exception("Service url missing");
    public static string LoginEmail => Instance.GetValue<string>("Authentication:Username") ?? throw new Exception("Login email is missing");
    public static string LoginPassword => Instance.GetValue<string>("Authentication:Password") ?? throw new Exception("Login password is missing");
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
    public static bool OutputPageResponse => Instance.GetValue<bool?>("OutputPageResponse") ?? false;

    /// <inheritdoc cref="IBrowserContext.GrantPermissionsAsync" />
    public static string[] Permissions => Instance.GetSection("Permissions")
        .GetChildren()
        .Where(c => !string.IsNullOrWhiteSpace(c.Value))
        .Select(c => c.Value!)
        .ToArray();
}