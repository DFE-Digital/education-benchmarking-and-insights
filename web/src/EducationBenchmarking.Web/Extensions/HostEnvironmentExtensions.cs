namespace EducationBenchmarking.Web.Extensions;

public static class HostEnvironmentExtensions
{
    private const string IntegrationEnvironment = "Integration";
    
    public static bool IsIntegration(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        return hostEnvironment.IsEnvironment(IntegrationEnvironment);
    }
}