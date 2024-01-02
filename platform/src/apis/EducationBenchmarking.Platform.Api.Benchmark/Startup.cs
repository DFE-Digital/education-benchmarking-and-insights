using System.Reflection;
using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Api.Benchmark.Validators;
using EducationBenchmarking.Platform.Shared;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.Benchmark;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());
        builder.Services.AddHealthChecks();
        
        builder.Services.AddOptions<BandingDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        
        builder.Services.AddSingleton<IComparatorSetDb,ComparatorSetDb>();
        builder.Services.AddSingleton<IBandingDb,BandingDb>();
        
        
        builder.Services.AddTransient<IValidator<ComparatorSetRequest>, ComparatorSetRequestValidator>();
    }
}