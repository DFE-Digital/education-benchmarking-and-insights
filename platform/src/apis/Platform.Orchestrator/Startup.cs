using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;
using Platform.Orchestrator;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Orchestrator;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddSerilogLoggerProvider(Constants.ApplicationName);

        builder.Services
            .AddOptions<JobStartMessageSenderOptions>()
            .BindConfiguration("PipelineMessageHub")
            .ValidateDataAnnotations();

        builder.Services
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations();

        builder.Services
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IJobStartMessageSender, JobStartMessageSender>()
            .AddSingleton<IPipelineDb, PipelineDb>();
    }
}