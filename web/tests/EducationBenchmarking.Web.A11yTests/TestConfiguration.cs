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

    public static string BaseUrl => Instance.GetValue<string>("ServiceUrl") ?? throw new Exception("Service url missing");
    
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
}