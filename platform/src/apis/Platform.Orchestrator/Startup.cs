using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Functions.Extensions;
using Platform.Orchestrator;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Orchestrator;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<JobStartMessageSenderOptions>().BindConfiguration("PipelineMessageHub").ValidateDataAnnotations();

        builder.Services.AddSingleton<IJobStartMessageSender, JobStartMessageSender>();
    }
}