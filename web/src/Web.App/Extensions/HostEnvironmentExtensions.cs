using System.Diagnostics.CodeAnalysis;

namespace Web.App.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HostEnvironmentExtensions
    {
        private const string IntegrationEnvironment = "Integration";

        public static bool IsIntegration(this IHostEnvironment hostEnvironment)
        {
            ArgumentNullException.ThrowIfNull(hostEnvironment);

            return hostEnvironment.IsEnvironment(IntegrationEnvironment);
        }
    }
}