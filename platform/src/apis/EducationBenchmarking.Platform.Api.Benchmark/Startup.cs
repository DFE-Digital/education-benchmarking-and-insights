using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Api.Benchmark.Validators;
using EducationBenchmarking.Platform.Domain.Requests;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;


[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<BandingDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<FinancialPlanDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<IComparatorSetDb, ComparatorSetDb>();
        builder.Services.AddSingleton<IBandingDb, BandingDb>();
        builder.Services.AddSingleton<IFinancialPlanDb, FinancialPlanDb>();

        builder.Services.AddTransient<IValidator<ComparatorSetRequest>, ComparatorSetRequestValidator>();
    }
}