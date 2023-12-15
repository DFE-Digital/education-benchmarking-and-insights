using Microsoft.AspNetCore.Mvc.Testing;

namespace EducationBenchmarking.Web.Integration.Tests;

public class BenchmarkingWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", "appinsights");
        builder.ConfigureServices(services => { });
        builder.UseEnvironment("IntegrationTest");
    }
}