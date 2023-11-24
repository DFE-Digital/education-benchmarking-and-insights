using Microsoft.AspNetCore.Mvc.Testing;

namespace EducationBenchmarking.Web.Integration.Tests
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // mocked services to be added here
            });

            builder.UseEnvironment("Development");
        }
    }
}
