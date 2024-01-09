using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Platform.ApiTests.TestSupport;

public static class Config
{
    public static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
            .AddJsonFile("appsettings.local.json", optional: false)
#endif
            .Build();

    public static Api Apis
    {
        get
        {
            var instance = new Api();
            Instance.Bind("Apis", instance);
            return instance;
        }
    }
    
    public class Api
    {
        public ApiEndpoint? Insight { get; set; }
        public ApiEndpoint? Benchmark { get; set; }
        public ApiEndpoint? Establishment { get; set; } 
        
        public class ApiEndpoint
        {
            public string? Host { get; set; } 
            public string? Key { get; set; }
        }
    }
}