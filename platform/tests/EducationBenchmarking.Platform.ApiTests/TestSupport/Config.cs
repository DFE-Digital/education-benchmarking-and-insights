using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Platform.ApiTests.TestSupport;

public class ApiEndpoint
{
    public string Host { get; set; } = null!;
    public string Key { get; set; } = null!;
}

public class Apis
{
    public ApiEndpoint Insight { get; set; } = null!;
    public ApiEndpoint Benchmark { get; set; } = null!;
}

public class Config
{
    public static IConfiguration Instance =>
        new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
            .AddJsonFile("appsettings.local.json", optional: false)
#endif
            .Build();

    public static Apis Apis
    {
        get
        {
            var instance = new Apis();
            Instance.Bind("Apis", instance);
            return instance;
        }
    }
}