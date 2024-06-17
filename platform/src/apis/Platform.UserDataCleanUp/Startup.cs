using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;
using Platform.UserDataCleanUp;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.UserDataCleanUp;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddSerilogLoggerProvider(Constants.ApplicationName);

        builder.Services
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations();

        builder.Services
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IPlatformDb, PlatformDb>();
    }
}