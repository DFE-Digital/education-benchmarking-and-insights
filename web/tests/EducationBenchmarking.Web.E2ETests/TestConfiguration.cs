using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Web.E2ETests;

public static class TestConfiguration
{
    private static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", optional: false)
#endif
        .Build();
    
    public static string ServiceUrl => Instance.GetValue<string>("ServiceUrl") ?? throw new Exception("Service url missing");
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
}