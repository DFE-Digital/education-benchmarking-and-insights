using Microsoft.Extensions.Configuration;

namespace EducationBenchmarking.Web.A11yTests;

public static class TestConfiguration
{
    private static IConfiguration Instance => new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
        .AddJsonFile("appsettings.local.json", optional: false)
#endif
        .Build();

    public static int PlanYear => Instance.GetValue<int?>("PlanYear") ?? DateTime.UtcNow.Year + 1;
    public static string School => Instance.GetValue<string>("SchoolUrn") ?? throw new Exception("Test school urn missing");
    public static IEnumerable<string> Impacts => Instance.GetSection("Impacts").Get<string[]>() ?? ["critical",  "serious"];
    public static string ServiceUrl => Instance.GetValue<string>("ServiceUrl") ?? throw new Exception("Service url missing");
    public static bool Headless => Instance.GetValue<bool?>("Headless") ?? true;
}