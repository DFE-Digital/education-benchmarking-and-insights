using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Web.E2ETests.TestSupport;

public static class Config
{
    public static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", optional: false)
#endif
        .Build();

    public static string BaseUrl => Instance.GetValue<string>("Urls:BaseUrl") ?? throw new Exception("Base url missing");
    
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
}