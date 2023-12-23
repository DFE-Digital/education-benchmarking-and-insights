namespace EducationBenchmarking.Web.Extensions;

public static class HostEnvironmentEnvExtensions
{
    private const string IntegrationTestEnvironment = "IntegrationTest";
    
    public static bool IsIntegrationTest(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        return hostEnvironment.IsEnvironment(IntegrationTestEnvironment);
    }
}