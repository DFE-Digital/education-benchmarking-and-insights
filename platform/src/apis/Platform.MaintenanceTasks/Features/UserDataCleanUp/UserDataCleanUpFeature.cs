using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.MaintenanceTasks.Features.UserDataCleanUp;

[ExcludeFromCodeCoverage]
public static class UserDataCleanUpFeature
{
    public static IServiceCollection AddUserDataCleanUpFeature(this IServiceCollection services)
    {
        return services.AddSingleton<IPlatformDb, PlatformDb>();
    }
}