namespace EducationBenchmarking.Web.A11yTests.TestSupport;
using Microsoft.Extensions.Configuration;

public class Config
{
    public static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", optional: false)
#endif
        .Build();

    public static string BaseUrl => Instance.GetValue<string>("Urls:BaseUrl")!;
}