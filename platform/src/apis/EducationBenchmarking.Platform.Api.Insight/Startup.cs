using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using EducationBenchmarking.Platform.Api.School;
using EducationBenchmarking.Platform.Api.School.Db;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.School;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        
        builder.Services.AddHealthChecks();
        
        builder.Services.AddOptions<CollectionServiceOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<AcademyDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<MaintainSchoolDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        
        builder.Services.AddSingleton<ISchoolExpenditureDb, SchoolExpenditureDb>();
        builder.Services.AddSingleton<ICollectionService, CollectionService>();
        builder.Services.AddSingleton<IMaintainSchoolDb, MaintainSchoolDb>();
        builder.Services.AddSingleton<IAcademyDb, AcademyDb>();
    }
}