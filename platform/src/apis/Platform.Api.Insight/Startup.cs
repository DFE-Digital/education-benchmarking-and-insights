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

        builder.Services.AddOptions<CollectionServiceOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<AcademyDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<MaintainSchoolDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolsDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<ISchoolsDb, SchoolsDb>();
        builder.Services.AddSingleton<ICollectionService, CollectionService>();
        builder.Services.AddSingleton<IMaintainSchoolDb, MaintainSchoolDb>();
        builder.Services.AddSingleton<IAcademyDb, AcademyDb>();
    }
}