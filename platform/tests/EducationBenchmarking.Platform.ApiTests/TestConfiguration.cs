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

    public static Api Apis
    {
        get
        {
            var instance = new Api();
            Instance.Bind("Apis", instance);
            return instance;
        }
    }
    
    public record  Api
    {
#nullable disable warnings
        public ApiEndpoint Insight { get; init; }
        
        public ApiEndpoint Benchmark { get; init; }

        public ApiEndpoint Establishment { get; init; }
        
        public record  ApiEndpoint
        {
            public string Host { get; init; }
            public string Key { get; init; }
        }
#nullable restore warnings
    }
}