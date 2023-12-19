using System.Reflection;
using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Api.Establishment.Search;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Infrastructure.Search.Validators;
using EducationBenchmarking.Platform.Shared;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.Establishment;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());
        
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<CollectionServiceOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolSearchServiceOptions>().BindConfiguration("Search").ValidateDataAnnotations();
        
        builder.Services.AddSingleton<ICollectionService, CollectionService>();
        builder.Services.AddSingleton<ISchoolDb, SchoolDb>();
        builder.Services.AddSingleton<ISearchService<School>, SchoolSearchService>();
        
        builder.Services.AddTransient<IValidator<PostSuggestRequest>, PostSuggestRequestValidator>();
    }
}