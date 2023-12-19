using System.Reflection;
using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Shared;
using EducationBenchmarking.Platform.Shared.Validators;
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
        
        builder.Services.AddSingleton<ISchoolDb, SchoolDb>();
        builder.Services.AddSingleton<ITrustDb, TrustDb>();
        
        builder.Services.AddTransient<IValidator<SchoolComparatorSetRequest>, SchoolComparatorSetRequestValidator>();
        builder.Services.AddTransient<IValidator<TrustComparatorSetRequest>, TrustComparatorSetRequestValidator>();
    }
}