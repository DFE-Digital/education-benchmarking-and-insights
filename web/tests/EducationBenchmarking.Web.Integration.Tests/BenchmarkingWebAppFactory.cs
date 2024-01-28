using Microsoft.AspNetCore.Mvc.Testing;

namespace EducationBenchmarking.Web.Integration.Tests;

public class BenchmarkingWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(_ => { });
        builder.UseEnvironment("Integration");
    }
}