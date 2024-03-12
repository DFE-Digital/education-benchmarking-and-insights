using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.Db;
using Platform.Functions.Extensions;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Api.Benchmark;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<FinancialPlanDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<IComparatorSetDb, ComparatorSetDb>();
        builder.Services.AddSingleton<IFinancialPlanDb, FinancialPlanDb>();
    }
}