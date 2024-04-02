using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<SchoolFinancesDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolsDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<CosmosDatabaseOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<ICosmosClientFactory, CosmosClientFactory>();
        builder.Services.AddSingleton<ISchoolFinancesDb, SchoolFinancesDb>();

        builder.Services.AddSingleton<ISchoolsDb, SchoolsDb>();
    }
}